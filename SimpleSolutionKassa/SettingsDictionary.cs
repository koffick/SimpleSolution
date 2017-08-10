using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSolutionKassa
{
    public class SettingsDictionary : Dictionary<string, string>
    {
        public SettingsDictionary()
        {
            this.Add("Сканер 2D", "SettingsXaml\\Scaner2D.xaml");
        }
    }

}
