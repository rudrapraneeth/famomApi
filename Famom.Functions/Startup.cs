using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using HomeMade.Infrastructure.Data.Configurations;
using HomeMade.Infrastructure.Data.DbContext;
using HomeMade.Infrastructure.Repositories;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

[assembly: FunctionsStartup(typeof(Famom.Functions.Startup))]
namespace Famom.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            string connectionString = Environment.GetEnvironmentVariable("connectionString");
            builder.Services.AddDbContext<FamomContext>(
              options => SqlServerDbContextOptionsExtensions.UseSqlServer(options, connectionString));

            builder.Services.AddTransient<IExpoRepository, ExpoRepository>();
            
            builder.Services.AddOptions<ServicebusConfig>()
                .Configure<IConfiguration>((settings, configuration) =>
                {
                    configuration.GetSection("SbConnectionString").Bind(settings);
                });
        }
    }
}
