using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        private List<string> BackStringArr = new List<string>();

        private List<Button> buttonArray = new List<Button>();

        private List<string> HistoryBackStrArr = new List<string>();
        
        public MainWindow()
        {
            InitializeComponent();
            buttonArray.Add(bNull);
            buttonArray.Add(bOne);
            buttonArray.Add(bTwo);
            buttonArray.Add(bThree);
            buttonArray.Add(bFour);
            buttonArray.Add(bFive);
            buttonArray.Add(bSix);
            buttonArray.Add(bSeven);
            buttonArray.Add(bEight);
            buttonArray.Add(bNine);
            buttonArray.Add(bFact);
            buttonArray.Add(bPow);
            buttonArray.Add(bLog);
            buttonArray.Add(bMod);
            buttonArray.Add(bDiv);
            buttonArray.Add(bDivr);
            buttonArray.Add(bMult);
            buttonArray.Add(bPlus);
            buttonArray.Add(bMinus);
            buttonArray.Add(bDot);
            buttonArray.Add(bPi);
            buttonArray.Add(bExp);
            buttonArray.Add(bLeftBr);
            buttonArray.Add(bRightBr);
            buttonArray.Add(bSin);
            buttonArray.Add(bCos);
            buttonArray.Add(bTan);
            buttonArray.Add(bCtg);
            buttonArray.Add(bEquall);
        }

        private void bClick(object sender, RoutedEventArgs e)
        {
            string content = (string)((Button)e.OriginalSource).DataContext;
            textBlock.Text += ButtonClickClass.ButtonClick(content, ref buttonArray, ref BackStringArr);
        }

        private void bNumbers_Click(object sender, RoutedEventArgs e)
        {
            string Content = (string)((Button)e.OriginalSource).DataContext.ToString();
            textBlock.Text = ButtonClickClass.NumbersClick(Content, textBlock.Text, ref buttonArray, ref BackStringArr, ref bDot);
        }

        private void bRemove_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = ButtonClickClass.RemoveClick(textBlock.Text, ref buttonArray, ref BackStringArr);
        }

        private void bClear_Click(object sender, RoutedEventArgs e)
        {
            textBlock.Text = "";
            BackStringArr.Clear();
            ButtonClickClass.ClearClick(ref buttonArray);
        }

        private void bEquall_Click(object sender, RoutedEventArgs e)
        {
            HistoryBackStrArr = FuncClass.CopyArray(BackStringArr);
            for(int i = 0; i<BackStringArr.Count; i++)
            {
                if(BackStringArr[i] == "pi")
                {
                    BackStringArr[i] = Math.PI.ToString();
                }
                else if(BackStringArr[i] == "e")
                {
                    BackStringArr[i] = Math.E.ToString();
                }
            }

            if (textBlock.Text.Split('(').Length - 1 != textBlock.Text.Split(')').Length - 1)
            {
                MessageBox.Show("Ошибка: не хватает — '(' или ')'");
                return;
            }
            ResultClass.Equall(0, BackStringArr.Count - 1, ref BackStringArr);
            if (!ResultClass.IsError)
            {
                textBlock.Text = BackStringArr[0];
            }    
            else
            {
                BackStringArr = FuncClass.CopyArray(HistoryBackStrArr);
                ResultClass.IsError = false;
            }
            HistoryBackStrArr.Clear();
        }
    }
}
