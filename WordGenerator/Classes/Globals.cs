using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

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

        public static void GetExData(string funcname, string formname, Exception ex)
        {
            string str = "";
            str += "קרתה תקלה בפורם:  " + formname + Environment.NewLine;
            str = "קרתה תקלה בפונקציה:  " + funcname + Environment.NewLine;
            str = "פרטי תקלה :   " + ex.Message + Environment.NewLine;
            str = "StackTrace: " + ex.StackTrace + Environment.NewLine;
            MessageBox.Show(str);
        }
    }
}
