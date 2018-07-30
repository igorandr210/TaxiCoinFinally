namespace TaxiCoinFinally.RequestObjectPatterns
{
    public class DeployControllerPattern : IControllerPattern
    {
        public string Sender { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public string PassPhrase { get; set; }
        public ulong Gas { get; set; }
    }
}
