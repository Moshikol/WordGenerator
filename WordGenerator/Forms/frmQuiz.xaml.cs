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
        frmMain frmMainGlob;

        public frmQuiz(List<Word> lstwords, frmMain frmmain)
        {
            frmMainGlob = frmmain;
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


                    RemoveWord();
                }
                else
                {// the meaning is worng and we want to add the Current word again To The list
                    txtMeaning.Background = Brushes.PaleVioletRed;
                    btnContinue.Visibility = Visibility.Visible;


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

        private void Window_Closed(object sender, EventArgs e)
        {
            frmMainGlob.Show();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            bool? ischecked = cbAddAgain.IsChecked;
            if (ischecked == true)
            {//if checked we want the word to come again
                AddWordAgain();
            }
            else
            {// if not checked it means we dont want the word to Come again
                RemoveWord();
            }
        }
    }
}
