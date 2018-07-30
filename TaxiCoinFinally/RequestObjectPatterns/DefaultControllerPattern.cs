namespace TaxiCoinFinally.RequestObjectPatterns
{
    public class DefaultControllerPattern : IControllerPattern
    {
        public ulong Gas { get; set; } = 2100000;
        public string Sender { get; set; }
        public string Password { get; set; }
        public string PassPhrase { get; set; }
    }
}
