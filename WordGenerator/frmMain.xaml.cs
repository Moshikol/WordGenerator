using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Shapes;
using WordGenerator.DataSets.WordsTableAdapters;

namespace WordGenerator
{
    /// <summary>
    /// Interaction logic for frmMain.xaml
    /// </summary>
    public partial class frmMain : Window
    {
        List<Word> lstWords = new List<Word>();
        public frmMain()
        {
            InitializeComponent();
            CmbFill();
            GetWordFromDb();
        }

        public void CmbFill()
        {
            List<string> lstStr = new List<string>();
            DAWords daWords = new DAWords();

            DataTable dt = daWords.GetAllLettersLst();
            lstStr.Add("בחר אותיות לבחינה");

            foreach (DataRow dr in dt.Rows)
            {

                lstStr.Add(dr["startingLet"].ToString());
            }

            cmbLetters.ItemsSource = lstStr;
           // cmbLetters.SelectedIndex = 0;
            //*****************Difflvl**************************

            List<string> lstDif = new List<string>();
            DADiffLevel daDiff = new DADiffLevel();

            DataTable dtdiff = daDiff.GetDiffLevelData();


            cmbDiffLvl.ItemsSource = DiffLevel.GetLstFromDb();




        }

        public void GetWordFromDb()
        {
            DAWords dawords = new DAWords();
            DataTable dtWords = dawords.GetWordsByDiffAndLetter("a", 1);
            foreach (DataRow dr in dtWords.Rows)
            {
                int temp;
                int.TryParse(dr["id"].ToString(), out temp);
                Word w = new Word(temp, dr["word"].ToString(), DiffLevel.GetDifflvlByID(dr["diffID"].ToString()), dr["meaning"].ToString());
                lstWords.Add(w);
            }

        }

        private void btnSaveAndStart_Click(object sender, RoutedEventArgs e)
        {
            List<DiffLevel> lstSelectedDiffs = new List<DiffLevel>();
           // lstSelectedDiffs = (List<DiffLevel>)cmbDiffLvl.SelectedItems;
            foreach (DiffLevel diff in cmbDiffLvl.SelectedItems)
            {
                lstSelectedDiffs.Add(diff);
            }



            List<string> lstSelectedLetters = new List<string>();
           // lstSelectedLetters = (List<string>)cmbLetters.SelectedItems;
            foreach (string str in cmbLetters.SelectedItems)
            {
                lstSelectedLetters.Add(str);
            }


        }
    }
}
