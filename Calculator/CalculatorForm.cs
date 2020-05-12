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
            AnalaizerClassDll.AnalaizerClass.Expression = "1+5*2+(10+10/2)*2+3-7+1*(10%3+100)";
            textBoxResult.Text = AnalaizerClassDll.AnalaizerClass.Estimate();
        }
    }
}
