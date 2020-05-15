using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AnalaizerClass.Exceptions;
using AnalaizerClassDll;

namespace Calculator
{
    public partial class Form1 : Form
    {
        private const int MAXTEXT = 65536;

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
    }
}