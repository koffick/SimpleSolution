using ConnectToTRIC.Common;
using ConnectToTRIC.PaymentOfBills;
using SimpleSolutionKassa.Common;
using SimpleSolutionKassa.Integration;
using SimpleSolutionKassa.Scaner2D;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Linq;
using System.Windows.Media;

namespace SimpleSolutionKassa.OperationsXaml
{
    /// <summary>
    /// Interaction logic for GkuTRIC.xaml
    /// </summary>
    public partial class GkuTRIC : Page
    {
        private decimal commissionPercent = 1.00M;
        private Scaner scaner;
        private DispatcherTimer dispatcherTimer;

        public GkuTRIC()
        {
            InitializeComponent();
            InitThread();
        }

        private void InitThread()
        {
            this.scaner = new Scaner(AppSettings.GetStringValue("Scaner2D"));
            this.scaner.InitDevice();
            this.scaner.Open();

            this.dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 2);
            dispatcherTimer.Start();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Parse(this.scaner.ReadData());
        }

        private void Parse(string data)
        {
            if (data != "")
            {
                var a = data.Split('|');
                if (a.Length > 0 && a[0] == "ST00012")
                {
                    MakeDefaultInputStates();
                    ParseResult(TicketParamsFactory.Make(a));
                }
            }
        }

        private void inputCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (InputCodeMask.OnlyDigital(ref e))
            {
                if (e.Key == Key.Enter)
                {
                    var key = (sender as TextBox).Text;
                    MakeDefaultInputStates();
                    SendMessageCheckLschet(key);
                }
                otherSumma.Visibility = System.Windows.Visibility.Hidden;
            }
        }

        private void SendMessageCheckLschet(string key)
        {
            var integration = new IntegrationToTRIC();
            integration.Init();
            var context = integration.ContextTRIC;
            var result = new CheckLschet(context).Run(key);
            ParseResult(TicketParamsFactory.Make(result));
        }

        private void ParseResult(Dictionary<string, string> result)
        {
            if ((result["account"] != null) && (result["account"].Length == 18))
            {
                barCode.Text = result["account"];
                lschet.Text = barCode.Text.Substring(0, 8);
            }
            else
                lschet.Text = result["account"];

            summa.Text = result["cur_balance"].ReplaceDecimalSeparator();
            //summaCommission.Text = MakeSummaCommission(summa.Text, this.commissionPercent);
            //summaTotal.Text = MakeSummaTotal(summa.Text, summaCommission.Text);
            address.Text = result["address"];
            family.Text = result["fio"];
            note.Text = result["note"];
            if ((result["ticketid"] != null) && (result["ticketid"] != "-1"))
                ticketid.Text = result["ticketid"];
            else
                ticketid.Text = "";
        }

        private string MakeSummaDecimal(string summa)
        {
            NumberStyles style;
            CultureInfo culture;
            decimal s_money = 0;
            culture = CultureInfo.CreateSpecificCulture("ru-RU");
            style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;
            if (Decimal.TryParse(summa, style, culture, out s_money))
            {
                return Decimal.Round(s_money, 2).ToString("F2", culture);
            }
            return "";
        }

        private string MakeSummaCommission(string summa, decimal commission)
        {
            NumberStyles style;
            CultureInfo culture;
            decimal s_money = 0;
            culture = CultureInfo.CreateSpecificCulture("ru-RU");
            style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;
            if (Decimal.TryParse(summa, style, culture, out s_money))
            {
                var sumComission = Decimal.Round(s_money * (commission / 100), 2);
                return Decimal.Round(sumComission, 2).ToString("F2", culture);
            }
            return "";
        }

        private string MakeSummaTotal(string summa, string commission)
        {
            NumberStyles style;
            CultureInfo culture;
            decimal s_money = 0;
            decimal s_money_commission = 0;
            decimal s_money_total = 0;
            culture = CultureInfo.CreateSpecificCulture("ru-RU");
            style = NumberStyles.Number | NumberStyles.AllowDecimalPoint;
            if (Decimal.TryParse(summa, style, culture, out s_money))
            {
                s_money_total = s_money_total + s_money;
            }
            if (Decimal.TryParse(commission, style, culture, out s_money_commission))
            {
                s_money_total = s_money_total + s_money_commission;
            }
            return Decimal.Round(s_money_total, 2).ToString("F2", culture);
        }

        private void MakeDefaultInputStates()
        {
            inputCode.Text = "";
            address.Text = "";
            family.Text = "";
            note.Text = "";
            barCode.Text = "";
            lschet.Text = "";
            summa.Text = "";
            summaCommission.Text = "";
            summaTotal.Text = "";
            ticketid.Text = "";
        }

        private void ticketid_TextChanged(object sender, TextChangedEventArgs e)
        {
            btnChangeSumm.IsEnabled = ticketid.Text != "";
            btnPrintTicket.IsEnabled = ticketid.Text != "";
        }

        private void btnChangeSumm_Click(object sender, RoutedEventArgs e)
        {
            btnChangeSumm.IsEnabled = false;
            if (barCode.Text != "")
            {
                var key = lschet.Text;
                MakeDefaultInputStates();
                SendMessageCheckLschet(key);
            }
            otherSumma.Visibility = System.Windows.Visibility.Visible;
            otherSumma.Focus();
        }

        private void otherSumma_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox tb = (sender as TextBox);
            string Decimalseparator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            if (InputCodeMask.OnlyMoney(tb.Text, ref e))
            {

                if (e.Key == Key.Enter)
                {
                    if ((tb.Text != "") && (tb.Text != Decimalseparator))
                    {
                        summa.Text = MakeSummaDecimal(tb.Text.ReplaceDecimalSeparator());
                        //summaCommission.Text = MakeSummaCommission(summa.Text, this.commissionPercent);
                        //summaTotal.Text = MakeSummaTotal(summa.Text, summaCommission.Text);
                    }
                    tb.Text = "";
                    tb.Visibility = System.Windows.Visibility.Hidden;
                    barCode.Focus();
                    btnChangeSumm.IsEnabled = ticketid.Text != "";
                }

                if (e.Key == Key.Decimal || e.Key == Key.OemPeriod)
                {
                    string text = tb.Text;
                    int pos = tb.SelectionStart;
                    text = text.Insert(pos, Decimalseparator);
                    tb.Text = text;
                    tb.SelectionStart = pos + 1;
                    e.Handled = true;
                }
            }
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            this.scaner.Close();
        }

        private void summa_TextChanged(object sender, TextChangedEventArgs e)
        {
            var summa = (sender as TextBox).Text;
            summaCommission.Text = MakeSummaCommission(summa, this.commissionPercent);
            summaTotal.Text = MakeSummaTotal(summa, summaCommission.Text);
        }

        private void summaTotal_TextChanged(object sender, TextChangedEventArgs e)
        {
            var control = sender as TextBox;
            if (Convert.ToDecimal(control.Text) > 30000) control.Foreground = new SolidColorBrush(Colors.Red);
            else
                control.Foreground = new SolidColorBrush(Colors.Black);


        }

    }
}
