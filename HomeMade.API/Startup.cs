using AutoMapper;
using HomeMade.Api.Extenstions;
using HomeMade.Api.Filters;
using HomeMade.Api.Models;
using HomeMade.Api.Utility;
using HomeMade.Core.Entities;
using HomeMade.Core.Interfaces;
using HomeMade.Infrastructure.Data.Configurations;
using HomeMade.Infrastructure.Data.DbContext;
using HomeMade.Infrastructure.IntegrationService;
using HomeMade.Infrastructure.Repositories;
using HomeMade.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;


namespace HomeMade.Api
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAzureAppConfiguration();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<FamomContext>(options => options.UseSqlServer(Configuration.GetSection("Famom:ConnectionStrings")["DbContext"]));
            services.AddScoped<FamomAuditContext, FamomAuditContext>();
            services.AddScoped<UsageAttribute>();
            services.Configure<AzureStorageConfig>(Configuration.GetSection("Famom:AzureStorageConfig"));
            services.Configure<JwtAuthenticationConfig>(Configuration.GetSection("Famom:Authentication"));
            services.Configure<TwilioConfig>(Configuration.GetSection("Famom:TwilioConfig"));
            services.Configure<ServicebusConfig>(Configuration.GetSection("Famom:ServicebusConfig"));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "My API", Version = "V1" });
            });
            //Dependency Injection
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddTransient<IKitchenRepository, KitchenRepository>();
            services.AddTransient<IOrderRepository, OrderRespository>();
            services.AddTransient<IAddressRepository, AddressRepository>();
            services.AddTransient<IExpoRepository, ExpoRepository>();
            services.AddTransient<IRatingsRepository, RatingsRepository>();
            services.AddTransient<ILookupRepository, LookupRepository>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IProfileRepository, ProfileRepository>();

            services.AddTransient<ITwilioService, TwilioService>();
            //services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Famom:Authentication:Issuer"],
                    ValidAudience = Configuration["Famom:Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Famom:Authentication:SecretKey"]))
                };
            });

            services.AddApplicationInsightsTelemetry(Configuration.GetSection("Famom:ApplicationInsights:InstrumentationKey"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Version 1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAzureAppConfiguration();
            loggerFactory.CreateLogger("homemadelogs");//.AddProvider(new AzureTableLoggerProvider());
                //(new AzureTableLoggerProvider("DefaultEndpointsProtocol=https;AccountName=homemadetable;AccountKey=DPSbDECoLjGKmc4786asluqZ6vuuvUNVqUAEyXcspUE2IXVgTwM2Oj2T9RJCIR9MV0eIE9xPh5pwEbc5Yhoqzw==;TableEndpoint=https://homemadetable.table.cosmos.azure.com:443/;", "LoggingSample"));
            app.ConfigureExceptionHandler();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthorization();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
