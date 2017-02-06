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
        public static bool isValidText(string text)
        {
            bool res = false;
            res = Regex.IsMatch(text, @"^[a-zA-Zא-ת]+$");
            return !res;
        }
    }
}
