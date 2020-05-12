using System;
using CalcClassDll;
using CalcClassDll.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcClassDllTests
{
    [TestClass]
    public class CalcTests
    {
        [TestMethod]
        public void TestMethodAdd()
        {
            Assert.AreEqual(Calc.Add(3, 23), 26);
        }

        [TestMethod]
        [ExpectedException(typeof(Error06))]
        public void TestMethodAddException()
        {
            double res = Calc.Add(double.MaxValue+double.MaxValue, 23);
        }

        [TestMethod]
        public void TestMethodSub()
        {
            Assert.AreEqual(Calc.Sub(3, 23), -20);
        }

        [TestMethod]
        [ExpectedException(typeof(Error06))]
        public void TestMethodSubException()
        {
            double res = Calc.Sub(double.MinValue-111111, double.MaxValue);
        }

        [TestMethod]
        public void TestMethodMult()
        {
            Assert.AreEqual(Calc.Mult(3, 23), 69);
        }

        [TestMethod]
        [ExpectedException(typeof(Error06))]
        public void TestMethodMultException()
        {
            double res = Calc.Mult(double.MaxValue, double.MaxValue);
        }

        [TestMethod]
        public void TestMethodDiv()
        {
            Assert.AreEqual(Calc.Div(9, 3), 3);
        }

        [TestMethod]
        [ExpectedException(typeof(Error09))]
        public void TestMethodDivException()
        {
            double res = Calc.Div(11, 0);
        }

        [TestMethod]
        public void TestMethodABS()
        {
            Assert.AreEqual(Calc.ABS(-112), 112);
        }

        //[TestMethod]
        //[ExpectedException(typeof(Error09))]
        //public void TestMethodABSException1()
        //{
        //    double res = Calc.ABS(double.MinValue);
        //}

        //[TestMethod]
        //[ExpectedException(typeof(Error09))]
        //public void TestMethodABSException2()
        //{
        //    double res = Calc.ABS(double.MaxValue + double.MaxValue);
        //}

        [TestMethod]
        public void TestMethodIABS()
        {
            Assert.AreEqual(Calc.IABS(12), -12);
        }

        //[TestMethod]
        //[ExpectedException(typeof(Error09))]
        //public void TestMethodIABSException1()
        //{
        //    double res = Calc.IABS(double.MinValue*double.MaxValue);
        //}

    }
}
