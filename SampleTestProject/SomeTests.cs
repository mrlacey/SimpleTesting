using System.Threading.Tasks;

namespace SampleTestProject
{
    public class SomeTests : SimpleTesting.IContainSimpleTests
    {
        readonly int internalInt;

        public SomeTests()
        {
            internalInt = 42;
        }


        public bool MyFirstTest()
        {
            return false; // red
        }

        public static bool MySecondTest()
        {
            return true;
        }

        public bool MyThirdTest() => true;

        public async Task<bool> MyFourthTest()
        {
            await Task.CompletedTask;
            return true;
        }
        public static async Task<bool> MyFifthTest()
        {
            await Task.CompletedTask;
            return true;
        }

        public bool CanUseClassConstrutorInTests()
        {
            return internalInt == 42;
        }
    }
}
