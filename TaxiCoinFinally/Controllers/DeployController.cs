using TaxiCoinFinally.RequestObjectPatterns;
using TaxiCoinFinally.Utils;
using System;
using System.Net;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using TokenAPI;
using System.Threading.Tasks;

namespace TaxiCoinFinally.Controllers
{
    public class DeployController : Controller
    {
        [Route("api/deploy")]
        [HttpPost]
        public async Task<JsonResult> Post([FromForm] DeployControllerPattern req)
        {
            object contractReceipt;
            Crypto.DecryptTwoStringsAndGetContractFunctions(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase, out ContractFunctions contractFunctions);
            try
            {
                contractReceipt=await contractFunctions.DeployContract(senderAddress, password, req.Gas);
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
