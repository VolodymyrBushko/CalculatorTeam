using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using AnalaizerClass.Exceptions;
using CalcClassDll.Exceptions;

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
            textBoxResult.Clear();
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (textBoxExpression.Text.Length > 0)
                textBoxExpression.Text = textBoxExpression.Text.Remove(textBoxExpression.Text.Length - 1);
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
            if (text == null || text.Length == 0) return;
            char last = text.Last();

            if (counter < 3 && "+-".Contains(last))
            {
                text = text.Remove(text.Length - 1);
                text += last == '+' ? '-' : '+';

                textBoxExpression.Text = text;
            }
            else if (counter > 3)
                textBoxExpression.Text = $"-({text})";

            counter = 0;
        }

        private void buttonEqaul_Click(object sender, EventArgs e)
        {
            AnalaizerClassDll.AnalaizerClass.Expression = textBoxExpression.Text;
            textBoxResult.Text = AnalaizerClassDll.AnalaizerClass.Estimate();
        }

        private void buttonMR_Click(object sender, EventArgs e)
        {
            try
            {
                double from = Convert.ToDouble(Clipboard.GetText());
                textBoxExpression.Text += from.ToString();
            }
            catch { textBoxResult.Text = "Content of Clipboard != numeric"; }
        }

        private void buttonMplus_Click(object sender, EventArgs e)
        {
            try
            {
                double from = Convert.ToDouble(Clipboard.GetText()),
                 numeric = Convert.ToDouble(textBoxResult.Text);

                Clipboard.SetData(DataFormats.Text, (from + numeric).ToString());
            }
            catch (Error06 ex) { textBoxResult.Text = ex.Message; }
            catch (Exception) { textBoxResult.Text = "Content of Clipboard != numeric or result != numeric"; }
        }

        private void buttonMC_Click(object sender, EventArgs e)
        {
            Clipboard.SetData(DataFormats.Text, "0");
        }
    }
}