using System;
using System.Collections.Generic;
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
using System.Data.SQLite;
using WordGenerator.DataSets;
using WordGenerator.DataSets.WordsTableAdapters;
using System.Data;
using System.Threading;
using System.Globalization;

namespace WordGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        DiffLevel diffGlob = new DiffLevel(-6, "");
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillCmbData();
        }

        public void FillCmbData()
        {
            cmbDiff.ItemsSource =  DiffLevel.GetLstFromDb();
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
                cmbDiff.SelectedIndex = 0;
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
            else e.Handled = false;
        }

        private void cmbDiff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            diffGlob = (DiffLevel)cmbDiff.SelectedItem;
        }

        private void txtWord_GotFocus(object sender, RoutedEventArgs e)
        {
            InputLanguageManager.Current.CurrentInputLanguage = new CultureInfo("en");
        }
    }
}
