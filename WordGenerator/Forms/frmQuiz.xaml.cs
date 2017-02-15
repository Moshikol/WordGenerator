using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WordGenerator.DataSets.WordsTableAdapters;

namespace WordGenerator
{
    /// <summary>
    /// Interaction logic for frmQuiz.xaml
    /// </summary>
    public partial class frmQuiz : Window
    {
        List<Word> lstWords = new List<Word>();
        Word CurrentWord;
        int index = 0;

        public frmQuiz(List<Word> lstwords)
        {
            lstWords = lstwords;
            InitializeComponent();
            DataContext = lstWords;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (index < lstWords.Count)
            {
                CurrentWord = lstWords[index];
                lblWord.Content = CurrentWord.theWord;
            }

        }

        private void txtMeaning_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public bool IsRight(int WordID)
        {
            bool res = false;
            DAWords dawords = new DAWords();
            DataTable dt = dawords.GetMeaningByWordID(WordID);
            if (dt.Rows[0]["meaning"].ToString() == txtMeaning.Text)
            {
                res = true;
            }


            return res;

        }




        private void txtMeaning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (IsRight(CurrentWord.ID))
                {// the meaning is Right  and we want to remove it from the list so we wont go over it again
                    txtMeaning.Background = Brushes.LightGreen;
                   
                   // txtMeaning.BorderThickness = new Thickness(3);
                    RemoveWord();
                }
                else
                {// the meaning is worng and we want to add the Current word again To The list
                    txtMeaning.Background = Brushes.PaleVioletRed;
                    Thread.Sleep(300);
                  // txtMeaning.BorderThickness = new Thickness(3);
                    AddWordAgain();
                }
            }


        }
        public void NextWord()
        {
            if (lstWords != null)
            {
                if (index < lstWords.Count)
                {
                   
                    index++;
                    CurrentWord = lstWords[index];
                    lblWord.Content = CurrentWord.theWord;
                  // txtMeaning.Background = Brushes.White;
                }
                else if ((index >= lstWords.Count) && (lstWords.Count > 0))
                {
                    index = 0;
                }
            }


        }


        public void AddWordAgain()
        {
            if (lstWords != null)
            {
                if (CurrentWord != null)
                {
                    lstWords.Add(CurrentWord);
                    NextWord();
                }

            }
        }

        public void RemoveWord()
        {
            if (lstWords != null)
            {
                if (CurrentWord != null)
                {
                    lstWords.Remove(CurrentWord);
                    NextWord();
                }

            }
        }

    }
}
