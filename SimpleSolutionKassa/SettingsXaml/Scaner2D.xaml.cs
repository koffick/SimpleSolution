using SimpleSolutionKassa.Scaner2D;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
  

namespace SimpleSolutionKassa.SettingsXaml
{
    /// <summary>
    /// Interaction logic for Scaner2D.xaml
    /// </summary>
    public partial class Scaner2D : Page
    {
        public Scaner2D()
        {
            InitializeComponent();
            get();
            var scanerProperty = AppSettings.GetStringValue("Scaner2D");
            var e = comPort.Items.OfType<string>().Where(w => w.Contains(scanerProperty)).FirstOrDefault();
            comPort.SelectedIndex = comPort.Items.IndexOf(e);
        }

        private void get()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%(COM[0-9]%'");
            List<string> ports = new List<string>();
            try
            {
                foreach (ManagementObject item in searcher.Get())
                {
                    foreach (var prop in item.Properties)
                    {
                        if (prop.Name == "Caption")
                        {
                            ports.Add(prop.Value.ToString());
                        }
                    }
                }
                comPort.ItemsSource = ports.ToArray();
            }
            catch (ManagementException)
            {
            }
        }

        private void comPort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var newString = e.AddedItems[0].ToString();
            var comPort = newString.Substring(newString.IndexOf("(COM")).Replace("(","").Replace(")","");
            AppSettings.SetStringValue("Scaner2D", comPort);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var scaner = new Scaner(AppSettings.GetStringValue("Scaner2D"));
            scaner.InitDevice();
            scaner.TestDevice();
        }
    }
}
