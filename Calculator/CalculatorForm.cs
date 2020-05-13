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

        private void button_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Text == "mod")
                textBoxExpression.Text += "%";
            else
                textBoxExpression.Text += (sender as Button).Text;
        }

        private void buttonC_Click(object sender, EventArgs e)
        {
            textBoxExpression.Clear();
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (textBoxExpression.Text.Length > 0)
                textBoxExpression.Text = textBoxExpression.Text.Remove(textBoxExpression.Text.Length - 1);
        }

        private void buttonEqaul_Click(object sender, EventArgs e)
        {
            AnalaizerClassDll.AnalaizerClass.Expression = textBoxExpression.Text;
            textBoxResult.Text = AnalaizerClassDll.AnalaizerClass.Estimate();
        }
    }
}
