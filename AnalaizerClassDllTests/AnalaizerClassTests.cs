using Microsoft.VisualStudio.TestTools.UnitTesting;
using AnalaizerClass.Exceptions;
using System.Collections;
using CalcClassDll.Exceptions;

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

        [TestMethod]
        public void CreateStack_FiveMulOpenFiveMulFiveClose_TrueReturned()
        {
            ArrayList actual = AnalaizerClassDll.AnalaizerClass.CreateStack("5 * (5 * 5)"),
                expected = new ArrayList(new string[] { "5", "5", "5", "*", "*" });

            Assert.AreEqual(expected.Count, actual.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(Error08))]
        public void CreateStack_BiggerForMaxLength_Error08Returned()
        {
            string expression = "1+1+1+1+1+1+1+1+1+1+1+1+1+1+1+1";
            AnalaizerClassDll.AnalaizerClass.CreateStack(expression);
        }

        [TestMethod]
        public void RunEstimate_OnePlusOne_TwoReturned()
        {
            ArrayList stack = AnalaizerClassDll.AnalaizerClass.CreateStack("1+1");
            string actual = AnalaizerClassDll.AnalaizerClass.RunEstimate(stack), expected = "2";

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Error03))]
        public void RunEstimate_MaxValuePlusMaxValue_Error03Returned()
        {
            ArrayList stack = AnalaizerClassDll.AnalaizerClass.CreateStack($"{double.MaxValue}+{double.MaxValue}");
            AnalaizerClassDll.AnalaizerClass.RunEstimate(stack);
        }

        [TestMethod]
        [ExpectedException(typeof(Error09))]
        public void RunEstimate_OneDividedByZero_Error09Returned()
        {
            ArrayList stack = AnalaizerClassDll.AnalaizerClass.CreateStack("1/0");
            AnalaizerClassDll.AnalaizerClass.RunEstimate(stack);
        }
    }
}