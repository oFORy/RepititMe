using Microsoft.Extensions.DependencyInjection;
using RepititMe.Application.Services.Reviews.Commands;
using RepititMe.Application.Services.Reviews.Queries;
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
                .AddScoped<IStudentCommandService, StudentCommandService>()

                .AddScoped<IUserQueryService, UserQueryService>()
                .AddScoped<IUserCommandService, UserCommandService>()

                .AddScoped<IReviewQueryService, ReviewQueryService>()
                .AddScoped<IReviewCommandService, ReviewCommandService>()
                ;
            return services;
        }
    }
}
