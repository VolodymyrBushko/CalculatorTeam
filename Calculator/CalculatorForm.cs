using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using AnalaizerClassDll;

namespace Calculator
{

    public partial class Form1 : Form
    {
        private double Memory = 0;
        private bool znakPlus = true;

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

        //Реалізувати обробники подій на кнопки: "+/-", "MR", "M+", "MC".
        private void buttonPlusMinus_Click(object sender, EventArgs e)
        {
            //int n = textBoxExpression.Text.Length;
            //Char x = textBoxExpression.Text.Last();
            //string tmp = textBoxExpression.Text;
            if (znakPlus == true)
            {
                znakPlus = false;
                textBoxExpression.Text = "-" + textBoxExpression.Text;
            }
            else if (znakPlus == false)
            {
                znakPlus = true;
                textBoxExpression.Text = "+" + textBoxExpression.Text;
            } 
        }

        private void buttonMplus_Click(object sender, EventArgs e)
        {
            Memory += Convert.ToInt32(textBoxResult.Text);
        } 

        private void buttonMC_Click(object sender, EventArgs e)
        {
            Memory = 0;
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            textBoxResult.Text = Memory.ToString();
        }

        private double GetLastNumber(string expression)
        {
            string forGetRes = "";
            int i = expression.Length - 1;
            double res;
            try
            {
                while (Convert.ToDouble(forGetRes + expression[i]) > double.MinValue && Convert.ToDouble(forGetRes + expression[i]) < double.MaxValue && i > -1)
                {
                    forGetRes += expression[i];
                }
            }
            catch (Exception)
            {  
            }
 
            res = Convert.ToDouble(forGetRes);
            return res;
        }


        private void buttonEqaul_Click(object sender, EventArgs e)
        {
            AnalaizerClassDll.AnalaizerClass.Expression = textBoxExpression.Text;
            textBoxResult.Text = AnalaizerClassDll.AnalaizerClass.Estimate();
        }
    }
}
