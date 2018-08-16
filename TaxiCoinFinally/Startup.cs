using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using TaxiCoinFinally.Contexts;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace TaxiCoinFinally
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
          var connectionString = Configuration.GetConnectionString("TaxiContext");
            services.AddEntityFrameworkNpgsql()
                .AddDbContext<TaxiContext>();// options => options.UseNpgsql(connectionString, b => b.MigrationsAssembly("TaxiCoinFinally")));
            services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TaxiContext>()
                .AddDefaultTokenProviders();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidIssuer = Configuration["JwtIssuer"],
                        ValidAudience = Configuration["JwtIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire
                    };
                });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,TaxiContext dbContext,IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            dbContext.Database.EnsureCreated();
            app.UseAuthentication();
            app.UseMvc(routes =>
            routes.MapRoute(name: "default",
                template: "api/{controller}/{action=Post}/{id?}"
                )
            );
            CreateUserRoles(serviceProvider).Wait();
        }

        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<User>>();
            var c=await UserManager.CreateAsync(new User() { UserName = "Admin", PublicKey = "0xf7323EAa895Ac13eAeBE5863CFf8471f41C9Be2a", PrivateKey = "0x470bfa337ed727bc0fa1615467e55dd88e646b8a5e2f4bfb4341740b68e417db" }, "@mainAdmin228@~");

            IdentityResult roleResult;
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck)
            {
                roleResult = await RoleManager.CreateAsync(new IdentityRole("User"));
            }
            User user = UserManager.Users.SingleOrDefault(b=>b.UserName=="Admin");
            await UserManager.AddToRoleAsync(user, "Admin");
            await UserManager.AddToRoleAsync(user, "User");
        }

    }
}
