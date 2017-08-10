using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSolutionKassa
{
    public class OperationsDictionary : Dictionary<string, string>
    {
        public OperationsDictionary()
        {
            this.Add("ЖКУ ОАО ТРИЦ", "OperationsXaml\\GkuTRIC.xaml");
            this.Add("Дополнительные услуги ОАО ТРИЦ", "OperationsXaml\\AdditionalServiceTRIC.xaml");
        }
    }

}
