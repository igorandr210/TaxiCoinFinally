using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using System;
using System.Net;
using System.Net.Http;
using TokenAPI;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaxiCoinFinally.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace TaxiCoinFinally.Controllers
{
    public class BalanceController : Controller
    {
        private UserManager<User> _userManager;

        //class constructor
        public BalanceController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet,Authorize]
        public async Task<string> Get()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ulong res;
            var contractFunctions = Crypto.GetContractFunctions();

            try
            {
                res =await contractFunctions.CallFunctionByName<System.UInt64>(user.PublicKey, user.PrivateKey, FunctionNames.Balance, user.PublicKey);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return res.ToString();
        }
    }
}
