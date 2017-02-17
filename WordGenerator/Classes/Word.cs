using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
