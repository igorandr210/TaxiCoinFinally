using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TokenAPI;
using Microsoft.AspNetCore.Identity;
using TaxiCoinFinally.Contexts;

namespace TaxiCoinFinally.Controllers
{
    public class RefundController : UserController
    {
        public RefundController(UserManager<User> userManager) : base(userManager)
        {
        }

        [HttpPost]
        public JsonResult Create(UInt64 id, [FromForm] DefaultControllerPattern req)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<int>.InvokeByTransaction( user, FunctionNames.Refund,req.Gas,funcParametrs:id);
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
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<int>.InvokeByTransaction(user, FunctionNames.ApproveRefund, req.Gas, funcParametrs: id);
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
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<int>.InvokeByTransaction(user, FunctionNames.DisApproveRefund, req.Gas, funcParametrs: id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }
    }
}
