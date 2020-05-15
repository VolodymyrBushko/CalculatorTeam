using AnalaizerClass.Exceptions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CalcClassDll;

namespace AnalaizerClassDll
{
	public static class AnalaizerClass
	{
		private const int MAXSTACK = 30;
		private static string operations = "+-*/%";
		public static string Expression { get; set; }

		public static bool CheckCurrency(string expression)
		{
			int open = expression.Count(x => x == '('),
				close = expression.Count(x => x == ')');

			if (open > close)
				throw new Error01(expression.LastIndexOf('('));
			else if (close > open)
				throw new Error01(expression.LastIndexOf(')'));

			return true;
		}

		public static string Format(string expression)
		{
			string result = string.Empty;
			expression = expression.Replace(" ", string.Empty);

			for (int i = 0; i < expression.Length; i++)
			{
				if ((i + 1 < expression.Length) && operations.Contains(expression[i]) && operations.Contains(expression[i + 1]))
					throw new Error04(i);

				if ((i + 2 < expression.Length) && (expression[i] == '(' && expression[i + 1] == '(' && expression[i + 2] == '('))
					throw new Error03();

				if (char.IsDigit(expression[i]) && i + 1 < expression.Length && expression[i + 1] == '(')
					result += $"{expression[i]} * ";
				else if (i == 0 && char.IsDigit(expression[1]) && (expression[0] == '+' || expression[0] == '-'))
					result += expression[i];
				else if ((expression[i] == '+' || expression[i] == '-') && i - 1 >= 0 && expression[i - 1] == '(')
					result += expression[i];
				else if (operations.Contains(expression[i]))
					result += $" {expression[i]} ";
				else if (char.IsDigit(expression[i]) || expression[i] == '(' || expression[i] == ')' || expression[i] == '.')
					result += expression[i];
				else
					throw new Error02(i);
			}

			if (operations.Contains(expression.Last()) || expression.Last() == '(')
				throw new Error05();

			return result;
		}

		public static ArrayList CreateStack(string expression)
		{
			int i = 0; string resultString = string.Empty;
			Stack<char> stackChar = new Stack<char>();

			while (i < expression.Length)
			{
				if (char.IsDigit(expression[i]) || expression[i] == '.'
					|| ((expression[0] == '+' || expression[0] == '-') && char.IsDigit(expression[1]) && i == 0)
					|| ((expression[i] == '+' || expression[i] == '-') && char.IsDigit(expression[i + 1]) && expression[i - 1] == '('))
					resultString += expression[i];
				else if (expression[i] == '(')
					stackChar.Push(expression[i]);
				else if (expression[i] == ')')
				{
					resultString += ' ';
					while (stackChar.Peek() != '(')
					{
						resultString += stackChar.Peek();
						resultString += ' ';
						stackChar.Pop();
					}
					stackChar.Pop();
				}
				else if (!char.IsDigit(expression[i]))
				{
					resultString += ' ';
					if (stackChar.Count > 0 && expression[i] == '+' || expression[i] == '-')
					{
						while (stackChar.Count > 0 && stackChar.Peek() != '(')
						{
							resultString += stackChar.Peek();
							resultString += ' ';
							stackChar.Pop();
						}
					}
					else if (stackChar.Count > 0 && expression[i] == '*' || expression[i] == '/')
					{
						while (stackChar.Count > 0 && stackChar.Peek() != '(' && stackChar.Peek() != '+' && stackChar.Peek() != '-')
						{
							resultString += stackChar.Peek();
							resultString += ' ';
							stackChar.Pop();
						}
					}
					stackChar.Push(expression[i]);
				}
				i++;
			}
			resultString += ' ';
			while (stackChar.Count > 0)
			{
				resultString += stackChar.Peek();
				resultString += ' ';
				stackChar.Pop();
			}

			ArrayList result = new ArrayList(resultString.Split(' '));

			while (result.Contains(""))
				result.Remove("");

			if (result.Count > MAXSTACK)
				throw new Error08();

			return result;
		}

		public static string RunEstimate(ArrayList stack)
		{
			Stack<double> stackDouble = new Stack<double>();
			string tmp = string.Empty, expression = string.Empty;
			int i = 0; double a = 0, b = 0;

			foreach (var item in stack)
				expression += $"{item} ";

			while (i < expression.Length)
			{
				if (char.IsDigit(expression[i]) || expression[i] == '.'
					|| ((expression[i] == '+' || expression[i] == '-') && char.IsDigit(expression[i + 1]))) // Якщо число з унарним +/-, то це коректно
					tmp += expression[i];
				else if (expression[i] == ' ' && i != (expression.Length - 1))
				{
					if (i != 0 && char.IsDigit(expression[i - 1]))
						stackDouble.Push(Convert.ToDouble(tmp));
					tmp = string.Empty;
				}
				else if (!char.IsDigit(expression[i]) && expression[i] != ' ' && expression[i] != '.')
				{
					try
					{
						a = stackDouble.Peek();
						stackDouble.Pop();
						b = stackDouble.Peek();
						stackDouble.Pop();

						switch (expression[i])
						{
							case '+': stackDouble.Push(Calc.Add(a, b)); break;
							case '-': stackDouble.Push(Calc.Sub(b, a)); break;
							case '*': stackDouble.Push(Calc.Mult(a, b)); break;
							case '/': stackDouble.Push(Calc.Div(b, a)); break;
							case '%': stackDouble.Push(Calc.Mod(b, a)); break;
						}
					}
					catch (InvalidOperationException) { return new Error03().Message; }
					catch (Exception ex) { return ex.Message; }
				}
				i++;
			}
			return stackDouble.Peek().ToString();
		}

		public static string Estimate()
		{
			CheckCurrency(Expression);
			return RunEstimate(CreateStack(Format(Expression)));
		}
	}
}