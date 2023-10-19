using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepititMe.Application.Services.Reviews.Common;
using RepititMe.Application.Services.Students.Common;
using RepititMe.Application.Services.Teachers.Common;
using RepititMe.Application.Services.Users.Common;
using RepititMe.Infrastructure.Persistence;
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
            services
                .AddScoped<IStudentRepository, StudentRepository>()
                .AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IReviewRepository, ReviewRepository>()
                .AddScoped<ITeacherRepository, TeacherRepository>()
                ;


            services
                .AddDbContext<BotDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly("RepititMe.Api")));

            /*services
                .AddDbContext<BotDbContext>(opt => opt.UseNpgsql(Environment.GetEnvironmentVariable("DB_CS"),
                b => b.MigrationsAssembly("RepititMe.Api")));*/

            return services;
        }
    }
}
