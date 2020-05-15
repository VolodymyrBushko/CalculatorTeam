using System;
using System.Collections;
using System.Collections.Generic;
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

        private void CheckEnterKeyPress(object sender, KeyPressEventArgs e)
        {
            // (-7  (3 * 3 / 1.1) + 17 + 3 - 12) * 3  (100 / 0.3)
            // -49272.7272727273
            try
            {
                if (e.KeyChar == (char)Keys.Return)
                {
                    AnalaizerClassDll.AnalaizerClass.Expression = textBoxExpression.Text;
                    textBoxResult.Text = AnalaizerClassDll.AnalaizerClass.Estimate();
                }
            }
            catch(Exception ex) { textBoxResult.Text = ex.Message; }
        }
    }
}
