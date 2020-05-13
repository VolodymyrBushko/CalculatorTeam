using AnalaizerClass.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CalcClassDll;

namespace AnalaizerClassDll
{
	public static class AnalaizerClass
	{
		private static int erposition = -1;
		private static string operations = "+-*/%";

		public static string Expression { get; set; }
		public static bool ShowMessage { get; set; }

		private static void SetError(int position)
		{
			erposition = position;
			ShowMessage = true;
		}

		public static bool CheckCurrency()
		{
			int balance = 0;
			erposition = 0;
			ShowMessage = false;

			for (int i = 0; i < Expression.Length; i++)
			{
				if (Expression[i].Equals('('))
					balance++;
				else if (Expression[i].Equals(')'))
					balance--;

				if (balance < 0)
				{
					erposition = i;
					ShowMessage = true;
					Calc.LastError = new Error01(erposition).Message;
					return false;
				}
			}

			if (balance != 0)
			{
				erposition = Expression.Length - 1;
				ShowMessage = true;
				Calc.LastError = new Error01(erposition).Message;
				return false;
			}

			return true;
		}

		public static string Format(string expression)
		{
			if (ShowMessage)
				return Calc.LastError;

			string result = string.Empty;
			string exp = expression.Replace(" ", string.Empty);

			for (int i = 0; i < exp.Length; i++)
			{
				if ((i + 1 < exp.Length) && operations.Contains(exp[i]) && operations.Contains(exp[i + 1]))
				{
					SetError(i);
					Calc.LastError = new Error04(erposition).Message;
					return Calc.LastError;
				}

				if (i == 0 && exp.Length > 2 && char.IsDigit(exp[1]) && (exp[0] == '+' || exp[0] == '-'))
					result += exp[i];
				else if ((exp[i] == '+' || exp[i] == '-') && exp[i - 1] == '(')
					result += exp[i];
				else if (operations.Contains(exp[i]))
					result += $" {exp[i]} ";
				else if (char.IsDigit(exp[i]) || exp[i] == '(' || exp[i] == ')')
					result += exp[i];
				else
				{
					SetError(i);
					Calc.LastError = new Error02(erposition).Message;
					return Calc.LastError;
				}
			}

			if (operations.Contains(exp.Last()) || exp.Last() == '(')
			{
				SetError(exp.Length - 1);
				Calc.LastError = new Error05().Message;
				return Calc.LastError;
			}

			return result;
		}

		public static ArrayList CreateStack(string exp)
		{
			if (ShowMessage)
				return null;

			int i = 0;
			Stack<char> stackChar = new Stack<char>();
			string tmp = string.Empty, str = string.Empty;

			str = exp.Replace(" ", string.Empty);

			while (i < str.Length)
			{
				if (str.Length > i + 1 && str[i] == '(' && (str[i + 1] == '+' || str[i + 1] == '-'))
				{
					tmp += str[++i];
					stackChar.Push(str[i - 1]);
				}
				else if (i == 0 && exp.Length > 2 && char.IsDigit(exp[1]) && (exp[0] == '+' || exp[0] == '-'))
					tmp += str[i];
				else if (char.IsDigit(str[i]) || str[i] == '.')
					tmp += str[i];
				else if (str[i] == '(')
					stackChar.Push(str[i]);
				else if (str[i] == ')')
				{
					tmp += ' ';
					while (stackChar.Peek() != '(')
					{
						tmp += stackChar.Peek();
						tmp += ' ';
						stackChar.Pop();
					}
					stackChar.Pop();
				}
				else if (!char.IsDigit(str[i]))
				{
					tmp += ' ';
					if (stackChar.Count != 0 && str[i] == '+' || str[i] == '-')
					{
						while (stackChar.Count != 0 && stackChar.Peek() != '(')
						{
							tmp += stackChar.Peek();
							tmp += ' ';
							stackChar.Pop();
						}
					}
					else if (stackChar.Count != 0 && str[i] == '*' || str[i] == '/')
					{
						while (stackChar.Count != 0 && stackChar.Peek() != '(' && stackChar.Peek() != '+' && stackChar.Peek() != '-' && stackChar.Peek() != '%')
						{
							tmp += stackChar.Peek();
							tmp += ' ';
							stackChar.Pop();
						}
					}
					stackChar.Push(str[i]);
				}
				i++;
			}
			tmp += ' ';
			while (stackChar.Count != 0)
			{
				tmp += stackChar.Peek();
				tmp += ' ';
				stackChar.Pop();
			}

			return new ArrayList(tmp.Split(' '));
		}

		public static string RunEstimate(ArrayList stack)
		{
			if (ShowMessage)
				return Calc.LastError;

			int i = 0;
			double a, b;
			string tmp = string.Empty, str = string.Empty;
			Stack<double> stackDouble = new Stack<double>();

			foreach (var item in stack)
				str += $"{item} ";

			while (i < str.Length)
			{
				if ((str[i] == '+' || str[i] == '-') && str.Length > i + 1 && char.IsDigit(str[i + 1]))
					tmp += str[i];
				else if (char.IsDigit(str[i]) || str[i] == '.')
					tmp += str[i];
				else if (str[i] == ' ' && i != (str.Length - 1))
				{
					if (i != 0 && char.IsDigit(str[i - 1]))
						stackDouble.Push(Convert.ToDouble(tmp));
					tmp = "";
				}
				else if (!char.IsDigit(str[i]) && str[i] != ' ' && str[i] != '.')
				{
					a = stackDouble.Peek();
					stackDouble.Pop();
					b = stackDouble.Peek();
					stackDouble.Pop();
					if (str[i] == '+')
						stackDouble.Push(a + b);
					else if (str[i] == '-')
						stackDouble.Push(b - a);
					else if (str[i] == '*')
						stackDouble.Push(a * b);
					else if (str[i] == '/')
						stackDouble.Push(b / a);
					else if (str[i] == '%')
						stackDouble.Push(b % a);
				}
				i++;
			}

			return stackDouble.Peek().ToString();
		}

		public static string Estimate()
		{
			CheckCurrency();
			return RunEstimate(CreateStack(Format(Expression)));
		}
	}
}