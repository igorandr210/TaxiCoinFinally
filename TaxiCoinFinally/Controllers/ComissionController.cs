using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using Nethereum.RPC.Eth.DTOs;
using TokenAPI;
using System.Threading.Tasks;

namespace TaxiCoinFinally.Controllers
{
    public class ComissionController: Controller
    {
        [HttpPost]
        public async Task<JsonResult> Post([FromForm] ComissionControllerPattern req)
        {
            Crypto.DecryptTwoStringsAndGetContractFunctions(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase, out ContractFunctions contractFunctions);
            TransactionReceipt res;
            try
            {
                res =await contractFunctions.CallFunctionByNameSendTransaction(senderAddress, password,FunctionNames.SetComission,req.Gas,parametrsOfFunction:req.Comission);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(res);
        }
    }
}
