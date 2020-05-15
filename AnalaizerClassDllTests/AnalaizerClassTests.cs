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
        public void CheckCurrency_TrueExpression_TrueReturned()
        {
            AnalaizerClassDll.AnalaizerClass.Expression = "10+50*(1+9)";
            bool res = AnalaizerClassDll.AnalaizerClass.CheckCurrency();
            Assert.AreEqual(res, true);
        }

        [TestMethod]
        public void CheckCurrency_FalseExpression_FalseAndError01Return()
        {
            AnalaizerClassDll.AnalaizerClass.Expression = "10+50*(1+9";
            bool res = AnalaizerClassDll.AnalaizerClass.CheckCurrency();
            Assert.AreEqual(res, false);
        }

        [TestMethod]
        public void Format_TrueExpression_TrueReturned()
        {
            string res = AnalaizerClassDll.AnalaizerClass.Format("10+ 50 * ( 1 +  9  )");
            Assert.AreEqual(res, "10 + 50 * (1 + 9)");
        }

        [TestMethod]
        public void Format_DoubleSymbols_Error04Returned()
        {
            string res = AnalaizerClassDll.AnalaizerClass.Format("10++ 50 * ( 1 +  9)");
            Assert.AreEqual(res.Substring(0, "Error04".Length), "Error04");
        }

        [TestMethod]
        public void Format_UnknownSymbols_Error02Returned()
        {
            string res = AnalaizerClassDll.AnalaizerClass.Format("@10+ 50 * ( 1 +  9)");
            Assert.AreEqual(res.Substring(0, "Error02".Length), "Error02");
        }

        [TestMethod]
        public void Format_LastSymbolsAsOperator_Error05Returned()
        {
            string res = AnalaizerClassDll.AnalaizerClass.Format("10+50*(1+9)+");
            Assert.AreEqual(res.Substring(0, "Error05".Length), "Error05");
        }

        [TestMethod]
        public void CreateStack_TrueExpression_ArrayListReturned()
        {
            ArrayList res = AnalaizerClassDll.AnalaizerClass.CreateStack("10+50*(1+9)");
            ArrayList ex = new ArrayList(new string[] { "10", "50", "1", "9", "+", "*", "+" });
            Assert.AreEqual(ex.Count, res.Count);
        }

        [TestMethod]
        public void RunEstimate_TrueArrayList_ResultReturned()
        {
            ArrayList list = new ArrayList(new string[] { "10", "50", "1", "9", "+", "*", "+" });
            string res = AnalaizerClassDll.AnalaizerClass.RunEstimate(list);
            Assert.AreEqual(510, res);
        }
    }
}