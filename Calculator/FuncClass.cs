using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Calculator
{
    static class FuncClass
    {
        public static List<string> CopyArray(List<string> array)
        {
            List<string> resultArray = new List<string>();

            foreach (string i in array)
            {
                resultArray.Add(i);
            }
            return resultArray;
        }

        public static Single FirstFunc(Single left, string func, Single right, ref bool isError)
        {
            switch (func)
            {
                case "*":
                    return left * right;
                case "/":
                    if (right == 0)
                    {
                        MessageBox.Show("Ошибка: Деление на ноль!");
                        isError = true;
                        return 0;
                    }
                    else
                    {
                        return left / right;
                    }

                case "mod":
                    if (right == 0)
                    {
                        MessageBox.Show("Ошибка: Деление на ноль!");
                        isError = true;
                        return 0;
                    }
                    else
                    {
                        return left % right;
                    }

                case "div":
                    if (right == 0)
                    {
                        MessageBox.Show("Ошибка: Деление на ноль!");
                        isError = true;
                        return 0;
                    }
                    else
                    {
                        return Convert.ToInt32(left / right);
                    }
                case "!":
                    if (left % Convert.ToInt32(left) == 0)
                    {
                        return Factorial(Convert.ToInt32(left));
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: в факториал передано не целое число!");//Можно добавить возможность вводить не целые числа
                        isError = true;
                        return 0;
                    }

                case "^":
                    return Single.Parse(Math.Pow(left, right).ToString());
                default:
                    return 0;
            }
        }

        private static Single Factorial(Single x)
        {
            if (x == 0)
            {
                return 1;
            }
            else
            {
                if (x < 0)
                {
                    return x * Factorial(x + 1);
                }
                else
                {
                    return x * Factorial(x - 1);
                }
            }
        }

        public static Single ResultFunctions(string func, double value, ref bool isError)
        {
            switch (func)
            {
                case "log":
                    if (value > 0)
                    {
                        return Convert.ToSingle(Math.Log10(Convert.ToDouble(value)));
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: логарифм от нуля или отрицательного числа!");
                        isError = true;
                        return 0;
                    }
                case "Sin":
                    return Convert.ToSingle(Math.Sin(value));
                case "Cos":
                    return Convert.ToSingle(Math.Cos(value));
                case "Tg":
                    try
                    {
                        return Convert.ToSingle(Math.Tan(value));
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка: в указанном значении тангенс не существует!");
                        isError = true;
                        return 1;
                    }
                case "Ctg":
                    try
                    {
                        return Convert.ToSingle(Math.Cos(value) / Math.Sin(value));
                    }
                    catch
                    {
                        MessageBox.Show("Ошибка: в указанном значении котангенс не существует!");
                        isError = true;
                        return 1;
                    }
                default:
                    return 0;
            }
        }

        public static Single TwoFunc(Single left, string func, Single right)
        {
            switch (func)
            {
                case "+":

                    return left + right;
                case "-":
                    return left - right;
                default:
                    return 0;
            }
        }


    }
}
