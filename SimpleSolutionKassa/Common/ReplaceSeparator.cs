using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSolutionKassa.Common
{
    public static class ReplaceSeparator
    {
        public static string ReplaceDecimalSeparator(this string text)
        {
            string Decimalseparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            text = text.Replace(".", Decimalseparator);
            text = text.Replace(",", Decimalseparator);
            text = text.Replace("б", Decimalseparator);
            text = text.Replace("ю", Decimalseparator);
            text = text.Replace("Б", Decimalseparator);
            text = text.Replace("Ю", Decimalseparator);
            return text;
        }
    }
}
