using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ConsoleApp11;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        MrDjo m = new MrDjo();
        int[,] testmas = { { 1, 2, 3, 4 }, { 4, 3, 2, 1 } };
        int i=0;
        [TestMethod]
        public void First()
        {
            Assert.IsTrue(m.Vivod(12, testmas));
        }
        [TestMethod]
        public void Second()
        {
            Assert.IsNull(m.str1);
        }
        [TestMethod]
        public void Three()
        {
            Assert.IsInstanceOfType(m.Vvod(ref i), typeof(int[,]));
        }
    }
}
