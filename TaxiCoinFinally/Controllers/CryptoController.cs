using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using Newtonsoft.Json;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;

namespace TaxiCoinFinally.Controllers
{
    public class CryptoController : Controller
    {
        [HttpPost]
        public JsonResult Encrypt([FromForm] DefaultControllerPattern req)
        {
            Crypto.EncryptTwoStrings(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase);
            CryptoResponsePattern cryptoResponse = new CryptoResponsePattern
            {
                PassPhrase = req.PassPhrase,
                Password = password,
                Sender = senderAddress
            };

            return Json(cryptoResponse);
        }

        [HttpPost]
        public JsonResult Decrypt([FromForm] DefaultControllerPattern req)
        {
            Crypto.DecryptTwoStrings(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase);
            CryptoResponsePattern cryptoResponse = new CryptoResponsePattern
            {
                PassPhrase = req.PassPhrase,
                Password = password,
                Sender = senderAddress
            };

            return Json(cryptoResponse);
        }
    }
}
