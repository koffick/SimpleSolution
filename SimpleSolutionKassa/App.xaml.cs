using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SimpleSolutionKassa
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public string fioPerson = "";
        public int pwdFiscal = 1;
        public MainWindow mainPage = null;


        public App()
        {
            new Login().ShowDialog();
            new MainWindow().Show();
        }
    }
}
