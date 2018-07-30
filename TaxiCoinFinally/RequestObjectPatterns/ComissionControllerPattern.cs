using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxiCoinFinally.RequestObjectPatterns
{
    public class ComissionControllerPattern:IControllerPattern
    {
        public ulong Comission { get; set; }
        public ulong Gas { get; set; } = 2100000;
        public string Sender { get; set; }
        public string Password { get; set; }
        public string PassPhrase { get; set; }
    }
}
