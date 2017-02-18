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
using WordGenerator;
using WordGenerator.DataSets.WordsTableAdapters;

namespace WordGenerator
{
    /// <summary>
    /// Interaction logic for frmMain.xaml
    /// </summary>
    public partial class frmMain : Window
    {
        List<Word> lstWords = new List<Word>();
        #region Events
        public frmMain()
        {
            InitializeComponent();
            CmbFill();

        }

        private void btnSaveAndStart_Click(object sender, RoutedEventArgs e)
        {
            if ((cmbLetters.SelectedItems.Count>0)&&(cmbDiffLvl.SelectedItems.Count>0))
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

                GetWordsFromDb(lstSelectedLetters, lstSelectedDiffs); 
            }
            else
            {
                MessageBox.Show("אנא בחר לפחות אות אחת ולפחות רמת קושי אחת");
            }
        }



        #endregion

        #region Functions

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

        private void GetWordsFromDb(List<string> lstSelectedLetters, List<DiffLevel> lstSelectedDiffs)
        {
            lstWords.Clear();
            DAWords dawords = new DAWords();
            foreach (DiffLevel diffLvl in lstSelectedDiffs)
            {

                foreach (string lettedr in lstSelectedLetters)
                {

                    DataTable dtWords = dawords.GetWordsByDiffAndLetter(lettedr, diffLvl.ID);
                    foreach (DataRow dr in dtWords.Rows)
                    {
                        int temp;
                        int.TryParse(dr["id"].ToString(), out temp);
                        Word w = new Word(temp, dr["word"].ToString(), DiffLevel.GetDifflvlByID(dr["diffID"].ToString()), dr["meaning"].ToString());
                        lstWords.Add(w);
                    }
                }

            }
           // lstWords = SuffleList(lstWords);

            frmQuiz quiz = new frmQuiz(lstWords,this);
            Hide();
            quiz.Show();

        }

        public List<Word> SuffleList(List<Word> lstW)
        {
            Random rnd = new Random();
            //  List tempwORD = lstW.OrderBy(item => rnd.Next());
            lstW.OrderBy(item => rnd.Next());
            foreach (Word w in lstW )
            {
                lstW.Add(w);
            }
            return lstW;
        }





        #endregion

        private void btnAddWords_Click(object sender, RoutedEventArgs e)
        {
            frmAddWords frmadd = new frmAddWords(this);
               Hide();
                frmadd.Show();
        }

        private void cmbLetters_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbLetters.SelectedIndex == 0)
            {
                e.Handled = false;
            }
        }
    }
}
