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
    public class RefundController : Controller
    {
        [HttpPost]
        public JsonResult Create(UInt64 id, [FromForm] DefaultControllerPattern req)
        {
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<int, DefaultControllerPattern>.InvokeByTransaction( req, FunctionNames.Refund,req.Gas,funcParametrs:id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult Approve(UInt64 id, [FromForm] DefaultControllerPattern req)
        {
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<int, DefaultControllerPattern>.InvokeByTransaction(req, FunctionNames.ApproveRefund, req.Gas, funcParametrs: id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }

        [HttpPost]
        public JsonResult DisApprove(UInt64 id, [FromForm] DefaultControllerPattern req)
        {
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<int, DefaultControllerPattern>.InvokeByTransaction(req, FunctionNames.DisApproveRefund, req.Gas, funcParametrs: id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }
    }
}
