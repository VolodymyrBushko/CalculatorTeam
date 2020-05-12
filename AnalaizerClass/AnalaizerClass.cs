using AnalaizerClass.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalaizerClassDll
{
	public static class AnalaizerClass
	{
		private static int erposition;

		public static string Expression { get; set; }
		public static bool ShowMessage { get; set; }

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
					return false;
				}
			}

			if (balance != 0)
			{
				erposition = Expression.Length - 1;
				ShowMessage = true;
				return false;
			}

			return true;
		}

		public static string Format()
		{
			string result = string.Empty;

			Expression.Replace(" ", string.Empty);

			foreach (char item in Expression)
				if (item == '+' || item == '-' || item == '*' || item == '/' || item == '%')
					result += $" {item} ";
				else
					result += item;

			return result;
		}

		public static ArrayList CreateStack(string expression)
		{
			int i = 0;
			Stack<char> stackChar = new Stack<char>();
			string tmp = string.Empty, str = string.Empty;

			str = expression.Replace(" ", string.Empty);

			while (i < str.Length)
			{
				if (char.IsDigit(str[i]) || str[i] == '.')
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
							stackChar.Peek();
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
			int i = 0; double a, b;
			string tmp = string.Empty, str = string.Empty;
			Stack<double> stackDouble = new Stack<double>();

			foreach (var item in stack)
				str += $"{item} ";

			while (i < str.Length)
			{
				if (char.IsDigit(str[i]) || str[i] == '.')
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
			Format();
			return RunEstimate(CreateStack(Expression));
		}
	}
}