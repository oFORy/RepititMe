using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RepititMe.Application.Common.ObjectRepository;
using RepititMe.Application.Services.Student.Queries;

namespace RepititMe.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddScoped<IStudentQueryService, StudentQueryService>()
                .AddScoped<IUserAccessQuery, UserAccessQuery>()
                //.AddScoped<IUnitTestsCommandService, UnitTestsCommandService>()
                //.AddScoped<IUnitTestsQueryService, UnitTestsQueryService>()
                ;
            return services;
        }
    }
}
