using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;

namespace SimpleTesting
{
    internal static class NamedTypeSymbolExtensions
    {
        public static string SimpleLocationText(this ISymbol symbol)
        {
            var location = symbol.Locations.First(); // SimpleLocation does not take care of partial stuff
            return $"'{symbol.Name}' on line {location.GetLineSpan().StartLinePosition.Line} of file '{location.SourceTree.FilePath}'";
        }

        public static IEnumerable<INamedTypeSymbol> GetNamedTypes(this INamespaceOrTypeSymbol sym)
        {
            if (sym is INamedTypeSymbol type)
            {
                yield return type;
            }

            foreach (var child in sym.GetMembers().OfType<INamespaceOrTypeSymbol>().SelectMany(GetNamedTypes))
            {
                yield return child;
            }
        }

        public static AttributeData FindAttribute(this INamedTypeSymbol type, INamedTypeSymbol attributeType)
            => type.GetAttributes().FirstOrDefault(attribute => attribute.AttributeClass.Equals(attributeType));

        public static string[] GetTypeArgumentNames(this ITypeSymbol typeSymbol)
        {
            return (typeSymbol as INamedTypeSymbol)?.TypeArguments.Select(ta => ta.MetadataName).ToArray() ?? new string[0];
        }

        public static string GetAvailableTypeArgument(this INamedTypeSymbol type, params string[] possibilities)
        {
            var alreadyUsed = type.GetTypeArgumentNames();

            var value = possibilities.FirstOrDefault(p => !alreadyUsed.Contains(p));
            if (value != null)
            {
                return value;
            }

            var fallback = possibilities.Last();
            for (var i = 0; ; i++)
            {
                value = fallback + i;
                if (!alreadyUsed.Contains(value))
                {
                    return value;
                }
            }
        }
    }
}
