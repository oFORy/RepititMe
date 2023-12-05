using Microsoft.Extensions.DependencyInjection;
using RepititMe.Application.Common;
using RepititMe.Application.Services.Admins.Commands;
using RepititMe.Application.Services.Admins.Common;
using RepititMe.Application.Services.Admins.Queries;
using RepititMe.Application.Services.Orders.Commands;
using RepititMe.Application.Services.Orders.Queries;
using RepititMe.Application.Services.Reports.Commands;
using RepititMe.Application.Services.Reports.Queries;
using RepititMe.Application.Services.Reviews.Commands;
using RepititMe.Application.Services.Reviews.Queries;
using RepititMe.Application.Services.Students.Commands;
using RepititMe.Application.Services.Students.Queries;
using RepititMe.Application.Services.Surveis.Queries;
using RepititMe.Application.Services.Teachers.Commands;
using RepititMe.Application.Services.Teachers.Queries;
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

                .AddScoped<ITeacherQueryService, TeacherQueryService>()
                .AddScoped<ITeacherCommandService, TeacherCommandService>()

                .AddScoped<IOrderQueryService, OrderQueryService>()
                .AddScoped<IOrderCommandService, OrderCommandService>()

                .AddScoped<ISurveyQueryService, SurveyQueryService>()

                .AddScoped<IReportQueryService, ReportQueryService>()
                .AddScoped<IReportCommandService, ReportCommandService>()

                .AddScoped<IAdminQueryService, AdminQueryService>()
                .AddScoped<IAdminCommandService, AdminCommandService>()

                .AddScoped<IPayment, Payment>()
                ;
            return services;
        }
    }
}
