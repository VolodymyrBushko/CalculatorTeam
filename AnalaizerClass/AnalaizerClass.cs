using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnalaizerClassDll
{
    public static class AnalaizerClass
    {
        private static int erposition;
        public static string Expression { get; set; }
        public static bool ShowMessage { get; set; }


        public static bool CheckCurrency()
        {
            ShowMessage = false;
            erposition = 0;
            int balance = 0;

            for (int i = 0; i < Expression.Length; i++)
            {
                if (Expression[i].Equals('('))
                    balance++;
                else if (Expression[i].Equals(')'))
                    balance--;

                if (balance < 0)
                {
                    erposition = i;
                    ShowMessage = true;
                    return false;
                }
            }

            if (balance != 0)
            {
                erposition = Expression.Length - 1;
                ShowMessage = true;
                return false;
            }

            return true;
        }
    }
}