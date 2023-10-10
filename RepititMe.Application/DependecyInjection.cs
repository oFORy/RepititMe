using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace RepititMe.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services
                //.AddScoped<IUserQueryService, UserQueryService>()
                //.AddScoped<IUserCommandService, UserCommandService>()
                //.AddScoped<IUnitTestsCommandService, UnitTestsCommandService>()
                //.AddScoped<IUnitTestsQueryService, UnitTestsQueryService>()
                ;
            return services;
        }
    }
}
