using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using Nethereum.RPC.Eth.DTOs;
using TokenAPI;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaxiCoinFinally.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace TaxiCoinFinally.Controllers
{
    public class ComissionController: Controller
    {
        private UserManager<User> _userManager;

        //class constructor
        public ComissionController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost,Authorize(Roles ="Admin"),Route("api/[controller]")]
        public async Task<JsonResult> Post([FromForm] ComissionControllerPattern req)
        {
            ContractFunctions contractFunctions = Globals.GetInstance().ContractFunctions;
            TransactionReceipt res;
            var user = await _userManager.GetUserAsync(HttpContext.User);
            try
            {
                res =await contractFunctions.CallFunctionByNameSendTransaction(user.PublicKey, user.PrivateKey,FunctionNames.SetComission,req.Gas,parametrsOfFunction:req.Comission);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(res);
        }
    }
}
