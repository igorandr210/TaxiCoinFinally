using TaxiCoinFinally.RequestObjectPatterns;
using Nethereum.RPC.Eth.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TokenAPI;

namespace TaxiCoinFinally.Utils
{
    public class TokenFunctionsResults<TResult, TPattern> where TPattern : IControllerPattern
    {
        public static TResult InvokeByCall(UInt64 id, TPattern req, string funcName, params object[] funcParametrs)
        {
            Crypto.DecryptTwoStringsAndGetContractFunctions(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase, out ContractFunctions contractFunctions);
            var param=new List<object>() { id };
            param.AddRange(funcParametrs);
            return contractFunctions.CallFunctionByName<TResult>(senderAddress, password, funcName, param.ToArray()).Result;
        }

        public static TResult InvokeByCall(TPattern req, string funcName)
        {
            Crypto.DecryptTwoStringsAndGetContractFunctions(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase, out ContractFunctions contractFunctions);

            return contractFunctions.CallFunctionByName<TResult>(senderAddress, password, funcName, null).Result;
        }


        public static TransactionReceipt InvokeByTransaction(TPattern req, string funcName,UInt64 Gas, params object[] funcParametrs)
        {
            Crypto.DecryptTwoStringsAndGetContractFunctions(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase, out ContractFunctions contractFunctions);

            return contractFunctions.CallFunctionByNameSendTransaction(senderAddress, password, funcName,Gas, funcParametrs).Result;
        }

        public static TransactionReceipt InvokeByTransaction(TPattern req, string funcName,UInt64 Value,UInt64 Gas)
        {
            Crypto.DecryptTwoStringsAndGetContractFunctions(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase, out ContractFunctions contractFunctions);

            return contractFunctions.CallFunctionByNameSendTransaction(senderAddress, password, funcName,Value:Value,Gas:Gas,parametrsOfFunction:null).Result;
        }

        public static TransactionReceipt InvokeByTransaction(TPattern req, string funcName,ulong Gas)
        {
            Crypto.DecryptTwoStringsAndGetContractFunctions(out string senderAddress, req.Sender, out string password, req.Password, req.PassPhrase, out ContractFunctions contractFunctions);

            return contractFunctions.CallFunctionByNameSendTransaction(senderAddress, password, funcName,Gas, null).Result;
        }
    }
}
