using HomeMade.Application.Interfaces;
using HomeMade.Application.Services;
using HomeMade.DataAccess.Context;
using HomeMade.DataAccess.Repositories;
using HomeMade.Domain.Repositories;
using HomeMade.Security;
using HomeMade.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HomeMade.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddTransient<ILoginRepository, LoginRepository>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddDbContext<HomeMadeDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultDbContext")));

            services.AddScoped<HomeMadeDbContext>();
        }
    }
}
