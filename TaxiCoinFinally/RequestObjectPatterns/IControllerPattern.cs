namespace TaxiCoinFinally.RequestObjectPatterns
{
    public interface IControllerPattern
    {
        string Sender { get; set; }
        string Password { get; set; }
        string PassPhrase { get; set; }
    }
}
