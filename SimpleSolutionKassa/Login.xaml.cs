using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SimpleSolutionKassa
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private App Main
        {
            get { return (Application.Current as App); }
        }

        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            Main.fioPerson = fioPerson.Text;
            Main.pwdFiscal = 1;
            Hide();
        }

        private void LoginPage_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            fioPerson.Focus();
        }
    }
}
