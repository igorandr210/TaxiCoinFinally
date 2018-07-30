using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using System;
using System.Net;
using System.Net.Http;
using TokenAPI;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace TaxiCoinFinally.Controllers
{
    public class BalanceController : Controller
    {
        [HttpPost]
        public async Task<string> Post([FromForm] DefaultControllerPattern req)
        {
            Crypto.DecryptTwoStringsAndGetContractFunctions(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase, out ContractFunctions contractFunctions);
            ulong res = 0;

            try
            {
                res =await contractFunctions.CallFunctionByName<System.UInt64>(senderAddress, password, FunctionNames.Balance, senderAddress);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return res.ToString();
        }
    }
}
