using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnalaizerClassDll;
using AnalaizerClass.Exceptions;
using System.Collections;

namespace AnalaizerClassDllTests
{
    [TestClass]
    public class AnalaizerClassTests
    {
        [TestMethod]
        public void CheckCurrency_OnePlusOpenOnePlusOneClose_TrueReturned()
        {
            bool expected = true,
                actual = AnalaizerClassDll.AnalaizerClass.CheckCurrency("1+(1+1)");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Error01))]
        public void CheckCurrency_OnePlusOpenOnePlusOne_Error01Returned()
        {
            AnalaizerClassDll.AnalaizerClass.CheckCurrency("1+(1+1");
        }

        [TestMethod]
        public void Format_OnePlusOpenOnePlusOneClose_FormatExpressionReturned()
        {
            string expected = "1 + (1 + 1)",
                actual = AnalaizerClassDll.AnalaizerClass.Format("1+(1+1)");

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Error04))]
        public void Format_OnePlusPlusOne_Error04Returned()
        {
            AnalaizerClassDll.AnalaizerClass.Format("1++1");
        }

        [TestMethod]
        [ExpectedException(typeof(Error03))]
        public void Format_OneOpenOpenOpenOnePlusOneCloseCloseClose_Error03Returned()
        {
            AnalaizerClassDll.AnalaizerClass.Format("1(((1+1)))");
        }

        [TestMethod]
        [ExpectedException(typeof(Error02))]
        public void Format_OneAmpersandOne_Error02Returned()
        {
            AnalaizerClassDll.AnalaizerClass.Format("1&1");
        }

        [TestMethod]
        [ExpectedException(typeof(Error05))]
        public void Format_OnePlusOnePlus_Error05Returned()
        {
            AnalaizerClassDll.AnalaizerClass.Format("1+1+");
        }
    }
}