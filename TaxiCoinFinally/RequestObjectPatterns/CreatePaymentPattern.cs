using System;

namespace TaxiCoinFinally.RequestObjectPatterns
{
    public class CreatePaymentPattern : IControllerPattern
    {
        public UInt64 Gas { get; set; } = 2100000;
        public UInt64 Value { get; set; }
        public string Sender { get; set; }
        public string Password { get; set; }
        public string PassPhrase { get; set; }
    }
}
