using System;
using System.Collections;
using System.Windows.Forms;
using AnalaizerClassDll;

namespace Calculator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonEqaul_Click(object sender, EventArgs e)
        {
            //AnalaizerClassDll.AnalaizerClass.Expression = "1+5*2+(10+10/2)*2+3-7+1*(10%3+100)";

            AnalaizerClassDll.AnalaizerClass.Expression = "(-5 * 3 + 11) + 55 / 17 - 11 * 12 * (3 + 100 / 2)";

            textBoxResult.Text = AnalaizerClassDll.AnalaizerClass.Estimate();
        }


    }
}
