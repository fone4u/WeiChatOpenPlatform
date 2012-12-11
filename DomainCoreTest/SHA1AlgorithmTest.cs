using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DomainCore;

namespace DomainCoreTest
{
    [TestClass]
    public class SHA1AlgorithmTest
    {
        [TestMethod]
        public void EnctyptTest()
        {
            String str = "HelloWorld";
            Assert.AreEqual(string.Empty, SHA1Algorithm.Enctypt(str));
        }
    }
}
