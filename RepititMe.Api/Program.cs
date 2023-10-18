using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RepititMe.Infrastructure;
using RepititMe.Application;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    builder.Services.AddControllers();
}



builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", false, true)
    .AddEnvironmentVariables();


var origins = builder.Configuration.GetSection("CorsOrigins:Urls").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "enablecorspolicy",
                      builder =>
                      {
                          builder.WithOrigins(origins);
                          builder.AllowAnyHeader();
                          builder.AllowAnyMethod();
                          builder.AllowCredentials();
                      });
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseWhen(context => context.Request.Path.StartsWithSegments("/Api/User/SignUpTeacher"), appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = 500_000_000;
        await next.Invoke();
    });
});

app.UseRouting();

app.UseCors();

app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers().RequireCors("enablecorspolicy");
});

app.Run();
