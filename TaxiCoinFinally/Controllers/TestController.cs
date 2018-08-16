using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxiCoinFinally.Contexts;
using Microsoft.EntityFrameworkCore;

namespace TaxiCoinFinally.Controllers
{
    
    public class TestController : Controller
    {
        public string Post()
        {
            return "OK!";
        }
    }
}
