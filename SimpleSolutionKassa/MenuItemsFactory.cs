using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SimpleSolutionKassa
{
    public static class MenuItemsFactory
    {
        public static void Fill(MenuItem mainMenu, Dictionary<string, string> menuItems, RoutedEventHandler handler)
        {
            foreach (var item in menuItems)
            {
                var mi = new MenuItem();
                mi.Header = item.Key;
                mi.Click += handler;
                mi.Visibility = Visibility.Visible;
                mainMenu.Items.Add(mi);
            }
        }
    }
}
