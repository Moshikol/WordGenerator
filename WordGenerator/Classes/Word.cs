using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordGenerator.DataSets.WordsTableAdapters;

namespace WordGenerator
{
   public class Word
    {
        public int ID { get; set; }
        public DiffLevel Difflvl { get; set; }
        public string theWord { get; set; }
      
        public string Meaning { get; set; }
        public char StartingLetter { get; set; }
        public int ErrorCount { get; set; }

        public Word( int id ,string TheWord , DiffLevel diff , string meaning)
        {
            ID = id;
            theWord = TheWord;
            Difflvl = diff;
            Meaning = meaning;
            StartingLetter = TheWord[0];

        }

        public static List<Word> GetLstFromDb()
        {
            List<Word> Reslst = new List<Word>();

            DAWords daWords = new DAWords();

            DataTable dt;
            dt = daWords.GetWordsData();
            foreach (DataRow dr in dt.Rows)
            {
                int tempid = 0;
                int.TryParse(dr["id"].ToString(), out tempid);
                DiffLevel df = DiffLevel.GetDifflvlByID(dr["diffID"].ToString());
                Reslst.Add(new Word(tempid, dr["word"].ToString(), df, dr["meaning"].ToString()));

            }



            return Reslst;
        }
    }
}
