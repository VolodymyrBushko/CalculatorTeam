using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalaizerClass.Exceptions;
using AnalaizerClassDll;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private const int MAXTEXT = 65536;
        private static bool isPressed = false;
        private static int counter = 0;

        public Form1(string[] args)
        {
            InitializeComponent();

            if (args.Length != 0)
            {
                foreach (string item in args)
                    textBoxExpression.Text += item;

                AnalaizerClassDll.AnalaizerClass.Expression = textBoxExpression.Text;
                textBoxResult.Text = AnalaizerClassDll.AnalaizerClass.Estimate();
            }
        }

        private void TextBoxKeyPress(object sender, KeyPressEventArgs e)
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
                else if (e.KeyChar == (char)Keys.Escape)
                    Close();
                else if (textBoxExpression.Text.Length > MAXTEXT)
                    throw new Error07();
            }
            catch (Exception ex) { textBoxResult.Text = ex.Message; }
        }

        private void buttonPlusMinus_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;

            Task.Run(() =>
            {
                while (isPressed)
                {
                    counter++;
                    Thread.Sleep(1000);
                }
            });
        }

        private void buttonPlusMinus_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;

            string text = textBoxExpression.Text.TrimEnd();
            char last = text.Last();

            if (counter < 3 && "+-".Contains(last))
            {
                text = text.Remove(text.Length - 1);

                if (last == '+' || last == '-')
                    text += last == '+' ? '-' : '+';

                textBoxExpression.Text = text;
            }
            else if (counter > 3)
                textBoxExpression.Text = $"-({text})";

            counter = 0;
        }
    }
}