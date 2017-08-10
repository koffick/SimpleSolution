using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SimpleSolutionKassa.Common
{
    public static class InputCodeMask
    {
        public static bool OnlyDigital(ref KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9)
                || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                || (e.Key == Key.Back)
                || (e.Key == Key.Enter)
                )
                )
            {
                e.Handled = true; // не выполнять процедуру
                return false;
            }
            return true;
        }

        public static bool OnlyMoney(string text, ref KeyEventArgs e)
        {
            if (!((e.Key >= Key.D0 && e.Key <= Key.D9)
                || (e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9)
                || (e.Key == Key.Decimal)
                || (e.Key == Key.OemPeriod)
                || (e.Key == Key.Back)
                || (e.Key == Key.Enter)
                )
                )
            {
                e.Handled = true; // не выполнять процедуру
                return false;
            }
            string Decimalseparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (text.Contains(Decimalseparator) && (e.Key == Key.Decimal || e.Key == Key.OemPeriod))
            {
                e.Handled = true; // не выполнять процедуру
                return false;
            }
            return true;
        }
    }
}
