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
            Assert.AreEqual("db8ac1c259eb89d4a131b253bacfca5f319d54f2", SHA1Algorithm.Enctypt(str));
        }
    }
}
