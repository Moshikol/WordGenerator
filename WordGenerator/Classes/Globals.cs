using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WordGenerator
{
    class Globals
    {
        public static bool isValidHebText(string text)
        {
            bool res = false;
            res = Regex.IsMatch(text, @"^[א-ת]+$");
            return !res;
        }

        public static bool isValidEnText(string text)
        {
            bool res = false;
            res = Regex.IsMatch(text, @"^[a-zA-Z]+$");
            return !res;
        }
    }
}
