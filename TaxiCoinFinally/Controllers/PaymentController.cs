using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TokenAPI;
using System.Threading.Tasks;

namespace TaxiCoinFinally.Controllers
{
    public class PaymentController : Controller
    {
        [HttpPost]
        public async Task<JsonResult> GetById(UInt64 id, [FromForm] DefaultControllerPattern req)
        {
            Crypto.DecryptTwoStringsAndGetContractFunctions(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase, out ContractFunctions contractFunctions);
            Payment res;
            try
            {
                res =await contractFunctions.DeserializePaymentById(senderAddress, password, id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(res);
        }
        [HttpPost]
        public JsonResult Create(UInt64 id, [FromForm] CreatePaymentPattern req)
        {
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<int, CreatePaymentPattern>.InvokeByTransaction(req, FunctionNames.CreatePayment,req.Gas ,new object[] { id, req.Value });
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }
    }
}
