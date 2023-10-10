using RepititMe.Application.Services.UnitTests.Common;
using RepititMe.Application.Services.Users.Common;
using RepititMe.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepititMe.Infrastructure
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            //services
                //.AddScoped<IUserRepository, UserRepository>()
                //.AddScoped<IUnitTestsRepository, UnitTestsRepository>()
                ;


            services
                .AddDbContext<BotDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("RepititMe.Api")));

            return services;
        }
    }
}
