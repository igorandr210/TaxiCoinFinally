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
    public class DepositController : UserController
    {
        public DepositController(UserManager<User> userManager) : base(userManager)
        {
        }

        [HttpPost]
        public JsonResult Post([FromForm] DepositPattern req)
        {
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<UInt64>.InvokeByTransaction(user, FunctionNames.Deposit, Value: req.Value, Gas: req.Gas);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }
    }
}
