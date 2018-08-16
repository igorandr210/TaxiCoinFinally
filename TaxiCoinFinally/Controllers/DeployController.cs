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
    
    public class DeployController : Controller
    {
        private UserManager<User> _userManager;

        //class constructor
        public DeployController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }


        
        [HttpPost,Authorize,Route("api/deploy")]
        public async Task<JsonResult> Post([FromForm] DeployControllerPattern req)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var email = user.Email;
            object contractReceipt;
            ContractFunctions contractFunctions;
            try
            {
                contractFunctions=Crypto.GetContractFunctions();
                contractReceipt =await contractFunctions.DeployContract(user.PublicKey,user.PrivateKey, req.Gas);
            }
            catch (Exception e)
            {
                return Json(e.Message);
            }

            return Json(contractReceipt);
        }
        [Route("api/deploy/fromaddress")]
        [HttpPost]
        public JsonResult GetApiFromContractAddress([FromForm] DeployControllerPattern req)
        {
            Globals.GetInstance().ContractFunctions.ContractAddress=req.Address;
            return Json(new { Status="OK!" });
        }
    }
}
