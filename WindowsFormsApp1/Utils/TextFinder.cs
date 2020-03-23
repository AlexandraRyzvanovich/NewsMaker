using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Utils
{
    class TextFinder
    {
        public int FindTextOccurances(string textToFind, string text)
        {
            int occurances = Regex.Matches(text, textToFind).Count;

            return occurances;
        }
    }
}
