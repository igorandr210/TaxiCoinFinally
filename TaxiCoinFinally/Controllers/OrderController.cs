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
    public class OrderController : Controller
    {
        [HttpPost]
        public JsonResult GetOrder(UInt64 id, [FromForm] DefaultControllerPattern req)
        {
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<TransactionReceipt,DefaultControllerPattern>.InvokeByTransaction( req, FunctionNames.GetOrder,req.Gas,funcParametrs:id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult CompleteOrder(UInt64 id, [FromForm] DefaultControllerPattern req)
        {
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<int, DefaultControllerPattern>.InvokeByTransaction(req, FunctionNames.CompleteOrder,req.Gas,funcParametrs:id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }
    }
}
