using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCoinFinally.RequestObjectPatterns
{
    public class DepositPattern:IControllerPattern
    {
        public string Sender { get; set; }
        public string Password { get; set; }
        public string PassPhrase { get; set; }
        public UInt64 Value { get; set; }
        public UInt64 Gas { get; set; } = 2100000;
    }
}
