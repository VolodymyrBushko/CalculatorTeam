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

        //Реалізувати обробники подій на кнопки: "+/-", "MR", "M+", "MC".
        //Якщо між сусідніми натисненнями на кнопку<+/-> проходить менше 3 секунд, то введений оператор міняється на протилежний.
        //Якщо між сусідніми натисненнями на кнопку <+/-> проходить більше 3 секунд, то до виразу дописується знак «-». 

        private void buttonPlusMinus_Click(object sender, EventArgs e)
        {
            //Task<Book> task2 = new Task(() =>
            //{
            //   
            //});
            //task2.Start();
            timer1.Start();
            if (timeForReactPlusMinus <= 3)
            {
                timeForReactPlusMinus = 0;
                //введений оператор міняється на протилежний.
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
                //до виразу дописується знак «-».  
                try
                {
                    textBoxExpression.Text = (double.Parse(textBoxExpression.Text) * (-1)).ToString();
                }
                catch (Exception)
                {
                    if (!string.IsNullOrEmpty(textBoxExpression.Text))//чи ця перевірка не зайва
                    {
                        if (textBoxExpression.Text.StartsWith("-"))
                        { 
                            textBoxExpression.Text = textBoxExpression.Text.TrimStart('-');
                            if (isMyBrecet)//треба забрати свої дужки  &  isMyBrecet=false;
                            {
                                //textBoxExpression.Text= textBoxExpression.Text.

                            //isMyBrecet=false;
                            }
                    }
                        else //if (textBoxExpression.Text.StartsWith("+"))
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
        //При натисненні на кнопку MR число з пам'яті приписується в кінець виразу в рядку «Вираз». 
        private void buttonMR_Click(object sender, EventArgs e)
        {
            textBoxExpression.Text += Memory.ToString();
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
