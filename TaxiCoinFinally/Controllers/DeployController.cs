using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TokenAPI;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using TaxiCoinFinally.Contexts;
using Microsoft.AspNetCore.Identity;

namespace TaxiCoinFinally.Controllers
{
    
    public class DeployController : UserController
    {
        public DeployController(UserManager<User> userManager) : base(userManager)
        {
        }

        [HttpPost,Authorize(Roles ="Admin"),Route("api/deploy")]
        public async Task<JsonResult> Post([FromForm] DefaultControllerPattern req)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            object contractReceipt;
            ContractFunctions contractFunctions;
            try
            {
                contractFunctions = Globals.GetInstance().ContractFunctions;
                contractReceipt =await contractFunctions.DeployContract(user.PublicKey,user.PrivateKey, req.Gas);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(contractReceipt);
        }
        
        [HttpPost,Authorize(Roles ="Admin"),Route("api/deploy/fromaddress")]
        public JsonResult GetApiFromContractAddress([FromForm] DeployControllerPattern req)
        {
            Globals.GetInstance().ContractFunctions.ContractAddress=req.Address;
            return Json(new { Status="OK!" });
        }
    }
}
