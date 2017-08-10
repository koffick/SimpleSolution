using ConnectToTRIC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSolutionKassa.Common
{
    public class TicketParamsFactory
    {
        public static Dictionary<string, string> Make(DisassembleResult onlineTicket)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("account", onlineTicket.GetAttr("account"));
            dic.Add("cur_balance", onlineTicket.GetAttr("cur_balance"));
            dic.Add("address", onlineTicket.GetAttr("address"));
            dic.Add("fio", onlineTicket.GetAttr("fio"));
            dic.Add("note", onlineTicket.GetAttr("note"));
            dic.Add("ticketid", onlineTicket.GetAttr("ticketid"));

            return dic;
        }

        public static Dictionary<string, string> Make(string[] scan2DTicket)
        {
            var dic = new Dictionary<string, string>();
            dic.Add("account", TextSearch(scan2DTicket, "UIN"));
            var summa = TextSearch(scan2DTicket, "Sum");
            string Decimalseparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (summa.Length == 1)
            {
                summa = "0" + Decimalseparator + "0" + summa;
            }
            if (summa.Length == 2)
            {
                summa = "0" + Decimalseparator + summa;
            }
            if (summa.Length > 2)
            {
                summa = summa.Insert(summa.Length - 2, Decimalseparator);
            } 
            dic.Add("cur_balance", summa);
            dic.Add("address", TextSearch(scan2DTicket, "PayerAddress"));
            dic.Add("fio", TextSearch(scan2DTicket, "LastName"));
            dic.Add("note", TextSearch(scan2DTicket, "Note"));
            dic.Add("ticketid", TextSearch(scan2DTicket, "DocNo"));

            return dic;
        }

        private static string TextSearch(string[] array, string key)
        {
            var s = array.Where(w => w.Contains(key)).FirstOrDefault();
            if (s != null && s != "")
            {
                return s.Substring(s.IndexOf('=') + 1);
            }
            return "";
        }

    }
}
