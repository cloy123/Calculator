using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows.Controls;

namespace Calculator
{
    static class ButtonClickClass
    {
        // что я не могу ввести
        private static readonly string[] arrFunctions = new string[]// sin cos tg ctg log
        {
            "!", "^", "mod", "log", "div","/", "*", "+", "-", "," ,
            "pi", "e", ")", "Sin", "Cos", "Tg", "Ctg",
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "="
        };

        private static string[] arrOperations = new string[]// + - * / div mod ^
        {
            "!", "^", "mod", "div", "/", "*", "+", "-", ",", ")","="

        };

        private static string[] arrFactorial = new string[]
        {
            "log","0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            "pi", "e", "(", ",", "Sin", "Cos", "Tg", "Ctg"
        };

        private static string[] arrPiandExp = new string[] // pi e
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "log",
            ",", "pi", "e","(", "Sin", "Cos","Tg", "Ctg"
        };

        private static string[] arrLeftBr = new string[]
        {
            "!", "^", "mod", "div","/",
            "*", "+", ",",")", "="
        };

        private static string[] arrRightBr = new string[]
        {
            "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
            "pi", "e", "log", "(", "Sin", "Cos", "Tg", "Ctg", ","
        };

        private static string[] arrDot = new string[]
        {
            "!", "^", "log", "mod", "div", "/", "*", "+", "-", ",",
            "pi", "e", "(", ")", "Sin", "Cos", "Tg", "Ctg", "="
        };

        private static string[] arrClear = new string[]
        {
            "!", "^", "mod", "div", "+", "*", "/", ")", ","
        };

        private static string[] arrNumbers = new string[]
        {
            "log", "pi", "e", "(", "Sin", "Cos", "Tg", "Ctg"
        };


        private static void OnOrOffButton(string[] arrContentButton, ref List<Button> bArray)
        {
            foreach (Button b in bArray)
            {
                if (arrContentButton.Contains(b.DataContext))
                {
                    b.IsEnabled = false;
                }
                else
                {
                    b.IsEnabled = true;
                }
            }
        }

        public static string ButtonClick(string content, ref List<Button> buttonArray, ref List<string> strArr)
        {
            switch(content)
            {
                //Functions sin cos tg ctg log
                case "Sin":
                    OnOrOffButton(arrFunctions, ref buttonArray);
                    break;
                case "Cos":
                    goto case "Sin";
                case "Tg":
                    goto case "Sin";
                case "Ctg":
                    goto case "Sin";
                case "log":
                    goto case "Sin";
                //Operations + - * / div mod ^
                case "+":
                    OnOrOffButton(arrOperations, ref buttonArray);
                    break;
                case "-":
                    goto case "+";
                case "*":
                    goto case "+";
                case "/":
                    goto case "+";
                case "div":
                    goto case "+";
                case "mod":
                    goto case "+";
                case "^":
                    goto case "+";
                //Factorial
                case "!":
                    OnOrOffButton(arrFactorial, ref buttonArray);
                    break;
                //LeftBr
                case "(":
                    OnOrOffButton(arrLeftBr, ref buttonArray);
                    break;
                //RightBr
                case ")":
                    OnOrOffButton(arrRightBr, ref buttonArray);
                    break;
                //PiandExp
                case "pi":
                    OnOrOffButton(arrPiandExp, ref buttonArray);
                    break;
                case "e":
                    goto case "pi";
                //ArrDot
                case ",":
                    OnOrOffButton(arrDot, ref buttonArray);
                    strArr[^1] += content;
                    return content;
            }
            strArr.Add(content);
            return content;
        }

        public static string RemoveClick(string text, ref List<Button> buttonArray, ref List<string> strArr)
        {
            if (strArr.Count != 0)
            {
                string lastChar = text[^1].ToString();
                if (strArr[^1].Length > 1 && (int.TryParse(lastChar, out _) || lastChar == ","))
                {
                    text = text.Remove(text.Length - 1);
                    strArr[^1] = strArr[^1].Remove(strArr[^1].Length - 1);
                }
                else
                {
                    string tempS = text;
                    for (int i = 0; i < strArr[strArr.Count - 1].Length; i++)
                    {
                        tempS = tempS.Remove(tempS.Length - 1);
                    }
                    strArr.RemoveAt(strArr.Count - 1);
                    text = tempS;
                }
                if (strArr.Count > 0)
                {
                    lastChar = text[^1].ToString();
                    string lastStr = strArr[^1];
                    if (int.TryParse(lastChar, out _))
                    {
                        OnOrOffButton(arrNumbers, ref buttonArray);
                    }
                    else if (lastChar == ",")
                    {
                        OnOrOffButton(arrDot, ref buttonArray);
                    }
                    else if (lastStr == "+" || lastStr == "-" || lastStr == "*" ||
                        lastStr == "/" || lastStr == "div" || lastStr == "mod" || lastStr == "^")
                    {
                        OnOrOffButton(arrOperations, ref buttonArray);
                    }
                    else if (lastStr == "log" || lastStr == "Sin" || lastStr == "Cos" || lastStr == "Tg" || lastStr == "Ctg")
                    {
                        OnOrOffButton(arrFunctions, ref buttonArray);
                    }
                    else if (lastStr == "!")
                    {
                        OnOrOffButton(arrFactorial, ref buttonArray);
                    }
                    else if (lastStr == "pi" || lastStr == "e")
                    {
                        OnOrOffButton(arrPiandExp, ref buttonArray);
                    }
                    else if (lastStr == "(")
                    {
                        OnOrOffButton(arrLeftBr, ref buttonArray);
                    }
                    else if (lastStr == ")")
                    {
                        OnOrOffButton(arrRightBr, ref buttonArray);
                    }
                }
                else
                {
                    OnOrOffButton(arrClear, ref buttonArray);
                }
            }
            return text;
        }

        public static void ClearClick(ref List<Button> buttonArray)
        {
            OnOrOffButton(arrClear, ref buttonArray);
        }

        public static string NumbersClick(string content, string text, ref List<Button> buttonArray, ref List<string> strArr, ref Button bDot)
        {
            OnOrOffButton(arrNumbers, ref buttonArray);
            if (strArr.Count > 0 && strArr[^1].Contains(","))
            {
                bDot.IsEnabled = false;
            }

            if (strArr.Count > 0)
            {
                string strLast = strArr[^1].ToString();
                string charLast = text[^1].ToString();
                if (charLast == "," || int.TryParse(charLast, out _))
                {
                    strArr[^1] += content;
                }
                else
                {
                    strArr.Add(content);
                }
                text += content;
            }
            else
            {
                strArr.Add(content);
                text += content;
            }
            return text;
        }
           
    }
}
