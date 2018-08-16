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
    public class BalanceController : UserController
    {
        public BalanceController(UserManager<User> userManager) : base(userManager)
        {
        }

        [HttpGet,Authorize,Route("api/[controller]")]
        public async Task<string> Get()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            ulong res;
            var contractFunctions = Globals.GetInstance().ContractFunctions;

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
