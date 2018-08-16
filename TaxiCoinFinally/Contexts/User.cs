using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaxiCoinFinally.Contexts
{
    public class User:IdentityUser
    {
        public string PublicKey { get; set; }
        public string PrivateKey { get; set; }
    }
}
