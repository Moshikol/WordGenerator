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
        List<Word> lstWorngWords = new List<Word>();
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
        private void Window_Closed(object sender, EventArgs e)
        {
            frmMainGlob.Show();
        }


        private void txtMeaning_TextChanged(object sender, TextChangedEventArgs e)
        {

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

                    SetErrorControls();
                    if (!(IsExistsonLst(CurrentWord.ID, lstWorngWords)))
                    {// IF the word dont exsits on the worng words list 
                        lstWorngWords.Add(CurrentWord);
                        CurrentWord.ErrorCount++;
                    }
                    else
                    {// IF the word do exsits on the worng words list 
                        CurrentWord.ErrorCount++;

                    }
                }
            }


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

        public void NextWord()
        {
            if (lstWords.Count > 0)
            {

                UnSetErrorControls();

                if (lstWords != null)
                {
                    if (index <= lstWords.Count - 1)
                    {

                        index++;
                        CurrentWord = lstWords[index - 1];
                        lblWord.Content = CurrentWord.theWord;
                        txtMeaning.Background = Brushes.White;
                        txtMeaning.Text = "";
                    }
                    else if ((index >= lstWords.Count - 1) && (lstWords.Count > 0))
                    {
                        index = 0;
                        //NextWord();
                    }
                }
            }
            else
            {//the Session ended all the words was done 
                ShowEndOfSessionControls();
            }


        }

        private void ShowEndOfSessionControls()
        {
            string strWrongWords="";
            foreach (Word w in lstWords)
            {
                if (w.ErrorCount>0)
                {
                    strWrongWords += Environment.NewLine + " המילה: " + w.theWord + Environment.NewLine + "הפירוש: " + w.Meaning +Environment.NewLine+"מספר הטעיות במילה: " + w.ErrorCount.ToString() ;
                }
            }
            lblScore.Content = strWrongWords;
            UnSetErrorControls();
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

        public void SetErrorControls()
        {
            lblMeaning.Content = "הפירוש:" + CurrentWord.Meaning;
            txtMeaning.Background = Brushes.PaleVioletRed;
            btnContinue.Visibility = Visibility.Visible;
            lblMeaning.Visibility = Visibility.Visible;
            cbAddAgain.Visibility = Visibility.Visible;
        }
        public void UnSetErrorControls()
        {
            lblMeaning.Content = "";
            txtMeaning.Background = Brushes.White;
            btnContinue.Visibility = Visibility.Collapsed;
            lblMeaning.Visibility = Visibility.Collapsed;
            cbAddAgain.Visibility = Visibility.Collapsed;
        }

        public bool IsExistsonLst(int WordID, List<Word> lstWordsToCheck)
        {
            bool res = false;
            foreach (Word w in lstWordsToCheck)
            {
                if (w.ID == WordID)
                {// THE WORD EXSISTS ON THE LIST
                    res = true;
                }

            }

            return res;

        }
    }
}
