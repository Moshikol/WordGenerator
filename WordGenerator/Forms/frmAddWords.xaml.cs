using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WordGenerator.DataSets.WordsTableAdapters;
using System.Data;
using System.Globalization;
using System;
using System.Linq;

namespace WordGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class frmAddWords : Window
    {
        frmMain frmMainGlob;
        Word GlobSelectedWord;
        public frmAddWords(frmMain main)
        {
            frmMainGlob = main;
            InitializeComponent();
        }
        DiffLevel diffGlob = new DiffLevel(-6, "");
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillCmbData();
            cmbDiff.ItemsSource = DiffLevel.GetLstFromDb();
            cmbDiff.SelectedIndex = 0;
        }

      



        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if ((txtWord.Text != "") && (txtMeaning.Text != "") && (cmbDiff.SelectedIndex != 0))
            {
                DAWords dawords = new DAWords();
                dawords.InsertWord(txtWord.Text, txtMeaning.Text, txtWord.Text.ToLower()[0].ToString(), diffGlob.ID);
                txtMeaning.Clear();
                txtWord.Clear();
                txtWord.Focus();
                //  cmbDiff.SelectedIndex = 0;
                FillCmbData();

            }
            else
                MessageBox.Show("אחד מהנתונים לא תקין אנא בדוק");
        }

        private void txtWord_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "")
            {
                e.Handled = Globals.isValidEnText(e.Text);
            }
            else e.Handled = false;
        }

        private void txtMeaning_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != " ")
            {
                e.Handled = Globals.isValidHebText(e.Text);
            }
            else e.Handled = true;
        }

        private void cmbDiff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            diffGlob = (DiffLevel)cmbDiff.SelectedItem;
        }

        private void txtWord_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("en");
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            frmMainGlob.Show();
        }

        private void cmbwords_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveWord();
            }
        }

        

        private void btnSaveWord_Click(object sender, RoutedEventArgs e)
        {
            SaveMeaning();

        }

        private void cmbwords_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (cmbwords.SelectedItem != null)
            {
                GlobSelectedWord = ((Word)cmbwords.SelectedItem);
                txtMeaningUpdate.Text = ((Word)cmbwords.SelectedItem).Meaning;
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            DAWords dawords = new DAWords();
            if (GlobSelectedWord != null)
            {
                txtMeaningUpdate.Text = "";
                dawords.DeleteWordbyid(GlobSelectedWord.ID);
                FillCmbData();
            }
        }


        public void FillCmbData()
        {

            cmbwords.ItemsSource = Word.GetLstFromDb();
        }

        private void SaveMeaning()
        {
            if (GlobSelectedWord != null)
            {
                DAWords dawords = new DAWords();
                dawords.UpdateMeaningOnly(txtMeaningUpdate.Text, (GlobSelectedWord.ID));
                FillCmbData();
                txtMeaningUpdate.Text = "";
            }
        }

        private void SaveWord()
        {
            if (GlobSelectedWord != null)
            {
                DAWords dawords = new DAWords();
                dawords.UpdateTheWordOnly(cmbwords.Text, (GlobSelectedWord.ID));
                FillCmbData();
            }
        }
    }
}
