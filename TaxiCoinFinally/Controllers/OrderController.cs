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
using Microsoft.AspNetCore.Authorization;

namespace TaxiCoinFinally.Controllers
{
    public class OrderController : UserController
    {
        public OrderController(UserManager<User> userManager) : base(userManager)
        {
        }

        [HttpPost, Authorize]
        public JsonResult GetOrder(UInt64 id, [FromForm] DefaultControllerPattern req)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<TransactionReceipt>.InvokeByTransaction( user, FunctionNames.GetOrder,req.Gas,funcParametrs:id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }

        [HttpPost, Authorize]
        public JsonResult CompleteOrder(UInt64 id, [FromForm] DefaultControllerPattern req)
        {
            var user =  _userManager.GetUserAsync(HttpContext.User).Result;
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<int>.InvokeByTransaction(user, FunctionNames.CompleteOrder,req.Gas,funcParametrs:id);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }
    }
}
