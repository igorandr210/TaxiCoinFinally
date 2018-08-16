using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TokenAPI;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaxiCoinFinally.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace TaxiCoinFinally.Controllers
{
    public class PaymentController : UserController
    {
        public PaymentController(UserManager<User> userManager) : base(userManager)
        {
        }

        [HttpGet,Authorize]
        public async Task<JsonResult> GetById(UInt64 id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ContractFunctions contractFunctions = Globals.GetInstance().ContractFunctions;
            Payment res;
            try
            { 
                res =await contractFunctions.DeserializePaymentById(user.PublicKey, user.PrivateKey, id);
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
            var user = _userManager.GetUserAsync(HttpContext.User).Result;
            TransactionReceipt result;
            try
            {
                result = TokenFunctionsResults<int>.InvokeByTransaction(user, FunctionNames.CreatePayment,req.Gas ,new object[] { id, req.Value });
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(result);
        }
    }
}
