using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{

    public partial class Form1 : Form
    {
        private double Memory = 0;
        int timeForReactPlusMinus=0;
        bool isMyBrecet = false;
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

        private void buttonPlusMinus_Click(object sender, EventArgs e)
        { 
            timer1.Start();
            if (timeForReactPlusMinus <= 3)
            {
                timeForReactPlusMinus = 0; 
                if (!string.IsNullOrEmpty(textBoxExpression.Text))
                {
                    string tmpString = textBoxExpression.Text.TrimEnd();
                    if (tmpString.EndsWith("-"))
                    {
                        tmpString = tmpString.TrimEnd('-');
                        textBoxExpression.Text = tmpString + "+";
                    }
                    else if (tmpString.EndsWith("+"))
                    {
                        tmpString = tmpString.TrimEnd('+');
                        textBoxExpression.Text = tmpString + "-";
                    }
                } 
            }
            else
            {
                timeForReactPlusMinus = 0;  
                try
                {
                    textBoxExpression.Text = (double.Parse(textBoxExpression.Text) * (-1)).ToString();
                }
                catch (Exception)
                {
                    if (!string.IsNullOrEmpty(textBoxExpression.Text)) 
                    {
                        if (textBoxExpression.Text.StartsWith("-"))
                        { 
                            textBoxExpression.Text = textBoxExpression.Text.TrimStart('-');
                            if (isMyBrecet) 
                            {
                                string strTMP = textBoxExpression.Text.Trim();
                                textBoxExpression.Text = (strTMP.Remove(0, 1)).Remove((strTMP.Length - 2), 1);
                                isMyBrecet =false;
                            }
                    }
                        else  
                        {
                            textBoxExpression.Text = "-(" + textBoxExpression.Text + ")";
                            isMyBrecet = true;
                        }
                    } 
                }
            }
        }

        private void buttonMplus_Click(object sender, EventArgs e)
        {
            try
            {
                Memory += double.Parse(textBoxResult.Text);
            }
            catch (Exception )
            {
                MessageBox.Show("Неможливо перетворити до числа");
            }  
        } 

        private void buttonMC_Click(object sender, EventArgs e)
        {
            Memory = 0;
        }
         
        private void buttonMR_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text += Memory.ToString();
        }
         
        private void buttonEqaul_Click(object sender, EventArgs e)
        {
            AnalaizerClassDll.AnalaizerClass.Expression = textBoxExpression.Text;
            textBoxResult.Text = AnalaizerClassDll.AnalaizerClass.Estimate();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeForReactPlusMinus++;
            if (timeForReactPlusMinus > 5)
            {
                timer1.Stop();
            }
        }                     
    }
}
