using ConnectToTRIC.Common;
using ConnectToTRIC.wsIntegrationTRIC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSolutionKassa.Integration
{
    public class IntegrationToTRIC
    {
        public Context ContextTRIC
        {
            get
            {
                return this.contextTRIC;
            }
        }
        
        private Context contextTRIC;

        public void Init()
        {
             contextTRIC = new Context(new ServiceIntegratorClient("WSHttpBinding_IServiceIntegrator"), "PayWebService", "paywebservice2012");
        }


    }
}
