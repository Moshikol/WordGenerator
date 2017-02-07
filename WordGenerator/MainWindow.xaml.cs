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
            DAWords dawords = new DAWords();
            dawords.InsertWord(txtWord.Text, txtMeaning.Text, txtWord.Text[0].ToString(), 1);
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
    }
}
