using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TokenAPI;

namespace TaxiCoinFinally.Controllers
{
    public class DepositController : Controller
    {
        [HttpPost]
        public JsonResult Post([FromForm] DepositPattern req)
        {
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<UInt64, DepositPattern>.InvokeByTransaction(req, FunctionNames.Deposit, Value: req.Value, Gas: req.Gas);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }
    }
}
