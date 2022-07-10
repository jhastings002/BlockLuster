using System;
using BlockLuster.Managers.Managers;
using BlockLuster.Managers.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using BlockLuster.Accessors.Interfaces;
using BlockLuster.Accessors.Accessors;

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
        }
    }
}
