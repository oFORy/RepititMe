using Microsoft.Extensions.DependencyInjection;
using RepititMe.Application.Services.Students.Commands;
using RepititMe.Application.Services.Students.Queries;
using RepititMe.Application.Services.Users.Commands;
using RepititMe.Application.Services.Users.Queries;

namespace RepititMe.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddScoped<IStudentQueryService, StudentQueryService>()
                .AddScoped<IUserQueryService, UserQueryService>()
                .AddScoped<IStudentCommandService, StudentCommandService>()
                .AddScoped<IUserCommandService, UserCommandService>()
                ;
            return services;
        }
    }
}
