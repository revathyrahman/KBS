using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MyLCIAutomation
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                Assert.IsTrue(true);
            }
            catch (AssertFailedException e)
            {

            }

        }
    }
}
