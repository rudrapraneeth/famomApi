using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureAppConfiguration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace HomeMade.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
                    {
                        var settings = config.Build();

                        config.AddAzureAppConfiguration(options =>
                        {
                            options.Connect(settings["ConnectionStrings:AppConfig"])
                                   .Select(KeyFilter.Any, LabelFilter.Null)
                                    // Override with any configuration values specific to current hosting env
                                   .Select(KeyFilter.Any, hostingContext.HostingEnvironment.EnvironmentName)
                                   .ConfigureRefresh(refresh =>
                                   {
                                       refresh.Register("Famom:Settings:Sentinel", refreshAll: true)
                                      .SetCacheExpiration(new TimeSpan(0, 3, 0));
                                   });
                        });
                    });
                    webBuilder.UseStartup<Startup>();
                });
    }
}
