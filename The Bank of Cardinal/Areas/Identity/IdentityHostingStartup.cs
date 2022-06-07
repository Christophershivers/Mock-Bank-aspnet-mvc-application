using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using The_Bank_of_Cardinal.Areas.Identity.Data;
using The_Bank_of_Cardinal.Data;

[assembly: HostingStartup(typeof(The_Bank_of_Cardinal.Areas.Identity.IdentityHostingStartup))]
namespace The_Bank_of_Cardinal.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CardinalDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CardinalDbContextConnection")));

                services.AddDefaultIdentity<CardinalUser>(options =>
                {

                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;

                }).AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<CardinalDbContext>();

                //services.AddDefaultIdentity<IdentityUser>().AddRoles<IdentityRole>().AddEntityFrameworkStores<CardinalDbContext>();
            });
        }
    }
}