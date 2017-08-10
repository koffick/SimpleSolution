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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MenuItemsFactory.Fill(OperationItem, new OperationsDictionary(), menuItem_Click);
            MenuItemsFactory.Fill(SettingItem, new SettingsDictionary(), menuItem_Click2);
        }
        public void menuItem_Click(object sender, RoutedEventArgs e)
        {
            var itemCaption = (MenuItem)sender;
            var xaml = new OperationsDictionary()[itemCaption.Header.ToString()].ToString();
            DataFrame.Navigate(new System.Uri(xaml, UriKind.RelativeOrAbsolute));
        }

        public void menuItem_Click2(object sender, RoutedEventArgs e)
        {
            var itemCaption = (MenuItem)sender;
            var xaml = new SettingsDictionary()[itemCaption.Header.ToString()].ToString();
            DataFrame.Navigate(new System.Uri(xaml, UriKind.RelativeOrAbsolute));
        }
    }
}
