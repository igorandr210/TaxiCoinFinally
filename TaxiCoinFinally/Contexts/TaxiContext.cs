
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TaxiCoinFinally.Contexts
{
    public class TaxiContext:IdentityDbContext<User>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=db;Database=taxi;Username=admin;Password=1515", b => b.MigrationsAssembly("TaxiCoinFinally"));
        }
        
    }
}
