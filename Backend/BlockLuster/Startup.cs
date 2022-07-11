using System;
using BlockLuster.Managers.Managers;
using BlockLuster.Managers.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using BlockLuster.Accessors.Interfaces;
using BlockLuster.Accessors.Accessors;
using BlockLuster.Common.SecurityService;
using BlockLuster.EntityFramework;
using Microsoft.AspNetCore.Identity;
using BlockLuster.Accessors.EntityFramework;
using Microsoft.EntityFrameworkCore;

[assembly: FunctionsStartup(typeof(BlockLuster.Client.Functions.Startup))]

namespace BlockLuster.Client.Functions
{
    internal class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<IMovieManager, MovieManager>();
            builder.Services.AddScoped<IUserManager, UserManager>();
            builder.Services.AddScoped<IUserAccessor, UserAccessor>();
            builder.Services.AddScoped<IMovieAccessor, MovieAccessor>();
            builder.Services.AddScoped<ISecurityService, SecurityService>();

            var config = builder.GetContext().Configuration;
            var connectionString = config["ConnectionStrings:Database"];
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

            builder.Services.AddIdentityCore<AspNetUser>()
                .AddEntityFrameworkStores<DatabaseContext>();
        }
    }
}
