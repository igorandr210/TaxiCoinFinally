using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Hex.HexTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TokenAPI
{
    [FunctionOutput]
    public class Payment
    {
        [Parameter("address", "Customer", 1)]
        public string Customer { get; set; }
        [Parameter("address", "Driver", 2)]
        public string Driver { get; set; }
        [Parameter("uint", "value", 3)]
        public UInt64 Value { get; set; }
        [Parameter("uint", "status", 4)]
        UInt64 Stat { get ; set; }
        [Parameter("bool", "refundApproved", 5)]
        public bool RefundApproved { get; set; }
        [Parameter("bool", "isValue", 6)]
        public bool IsValue { get; set; }
        public string Status { get { return Enum.GetName(typeof(PaymentStatus), Stat); } set { } }
    }
}
