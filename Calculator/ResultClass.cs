using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Documents;

namespace Calculator
{
    static class ResultClass
    {
        public static bool IsError = false;
        private static Single result;

        public static void Equall(int firstIndex, int lastIndex,ref List<string> Array)
        {
            int i = firstIndex;
            while (i < lastIndex)
            {
                if (Array[i] == "(")
                {
                    int j = i;
                    int sumLeftBr = 1;
                    int sumRightBr = 0;
                    while (sumLeftBr != sumRightBr)
                    {
                        j++;
                        if (Array[j] == "(")
                        { sumLeftBr += 1; }
                        if (Array[j] == ")")
                        { sumRightBr += 1; }
                    }
                    int CountArray = Array.Count;
                    Equall(i + 1, j - 1, ref Array);
                    lastIndex -= CountArray - Array.Count;
                }
                i++;
            }
            if (firstIndex == lastIndex)
            {
                if (firstIndex - 1 >= 0 && lastIndex + 1 < Array.Count && Array[firstIndex - 1] == "(" && Array[lastIndex + 1] == ")")
                {
                    Array[firstIndex - 1] = Array[firstIndex];
                    Array.RemoveRange(firstIndex, 2);
                    return;
                }
            }
            if (firstIndex - 1 < 0 || Array[firstIndex - 1] != "(")
            {
                Array[firstIndex] = MainResult(firstIndex, lastIndex, ref Array).ToString();
                result = Single.Parse(Array[firstIndex]);
                Array.RemoveRange(firstIndex + 1, lastIndex - firstIndex);
            }
            else
            {
                Array[firstIndex - 1] = MainResult(firstIndex, lastIndex, ref Array).ToString();
                result = Single.Parse(Array[firstIndex - 1]);
                Array.RemoveRange(firstIndex, lastIndex - firstIndex + 2);
            }
        }

        private static Single MainResult(int fIndex, int lIndex, ref List<string> Array)
        {
            Single tempResult = 0;
            List<string> tempArray = new List<string>();

            for (int i = fIndex; i <= lIndex; i++)
            {
                tempArray.Add(Array[i]);
            }
            if (tempArray.Count == 1)
            {
                Single.TryParse(tempArray[0], out tempResult);
                return tempResult;
            }
            if (!Single.TryParse(tempArray[0], out result))
            {
                tempResult = Single.Parse(tempArray[1]);
            }
            int fI = 0;

            while (fI < tempArray.Count)
            {
                if (tempArray[fI] == "log" || tempArray[fI] == "Sin" || tempArray[fI] == "Cos"
                    || tempArray[fI] == "Tg" || tempArray[fI] == "Ctg")
                {
                    tempResult = FuncClass.ResultFunctions(tempArray[fI], double.Parse(tempArray[fI + 1]), ref IsError);
                    tempArray[fI] = tempResult.ToString();
                    tempArray.RemoveAt(fI + 1);
                    fI = 0;
                }
                fI++;
            }
            fI = 0;

            while (fI < tempArray.Count)
            {
                if (tempArray[fI] == "!")
                {
                    tempResult = FuncClass.FirstFunc(Single.Parse(tempArray[fI - 1]), tempArray[fI], 0, ref IsError);
                    tempArray[fI - 1] = tempResult.ToString();
                    tempArray.RemoveAt(fI);
                    fI = 0;
                }
                fI++;
            }
            fI = 0;

            while (fI < tempArray.Count)
            {
                if (tempArray[fI] == "^")
                {
                    tempResult = FuncClass.FirstFunc(Single.Parse(tempArray[fI - 1]), tempArray[fI], Single.Parse(tempArray[fI + 1]), ref IsError);
                    tempArray[fI - 1] = tempResult.ToString();
                    tempArray.RemoveRange(fI, 2);
                    fI = 0;
                }
                fI++;
            }
            fI = 0;

            while (fI < tempArray.Count)
            {
                if (tempArray[fI] == "*" || tempArray[fI] == "/" || tempArray[fI] == "div" ||
                    tempArray[fI] == "mod")
                {
                    tempResult = FuncClass.FirstFunc(Single.Parse(tempArray[fI - 1]), tempArray[fI], Single.Parse(tempArray[fI + 1]), ref IsError);
                    tempArray[fI - 1] = tempResult.ToString();
                    tempArray.RemoveRange(fI, 2);
                    fI = 0;
                }
                fI++;
            }
            fI = 0;

            while (fI < tempArray.Count)
            {
                if (tempArray[fI] == "+" || tempArray[fI] == "-")
                {
                    if (tempArray[fI] == "-" && (fI - 1 < 0 || !int.TryParse(Array[fI - 1], out _)))
                    {
                        tempResult = FuncClass.TwoFunc(0, tempArray[fI], Single.Parse(tempArray[fI + 1]));
                        tempArray[fI + 1] = tempResult.ToString();
                        tempArray.RemoveAt(fI);
                    }
                    else
                    {
                        tempResult = FuncClass.TwoFunc(Single.Parse(tempArray[fI - 1]), tempArray[fI], Single.Parse(tempArray[fI + 1]));
                        tempArray[fI - 1] = tempResult.ToString();
                        tempArray.RemoveRange(fI, 2);
                    }
                    fI = 0;
                }
                fI++;
            }
            return tempResult;
        }
    }
}
