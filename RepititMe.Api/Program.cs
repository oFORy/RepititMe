using Microsoft.Extensions.Configuration;
using RepititMe.Infrastructure;
using RepititMe.Application;
using Microsoft.AspNetCore.Http.Features;
using Telegram.Bot;
using RepititMe.Application.bot;
using RepititMe.Application.bot.Services;

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





// There are several strategies for completing asynchronous tasks during startup.
// Some of them could be found in this article https://andrewlock.net/running-async-tasks-on-app-startup-in-asp-net-core-part-1/
// We are going to use IHostedService to add and later remove Webhook
/*builder.Services.AddHostedService<ConfigureWebhook>();

// Register named HttpClient to get benefits of IHttpClientFactory
// and consume it with ITelegramBotClient typed client.
// More read:
//  https://docs.microsoft.com/en-us/aspnet/core/fundamentals/http-requests?view=aspnetcore-5.0#typed-clients
//  https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests
builder.Services.AddHttpClient("tgwebhook")
        .AddTypedClient<ITelegramBotClient>(httpClient
            => new TelegramBotClient(Environment.GetEnvironmentVariable("BotToken"), httpClient));


// The Telegram.Bot library heavily depends on Newtonsoft.Json library to deserialize
// incoming webhook updates and send serialized responses back.
// Read more about adding Newtonsoft.Json to ASP.NET Core pipeline:
//   https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/formatting?view=aspnetcore-6.0#add-newtonsoftjson-based-json-format-support
builder.Services.AddControllers().AddNewtonsoftJson();

builder.Services.AddScoped<HandlerService>();
builder.Services.AddScoped<ITelegramService, TelegramService>();

var botToken = Environment.GetEnvironmentVariable("BotToken");*/













var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseWhen(context => context.Request.Path.StartsWithSegments("/Api/Teacher/ChangeProfile"), appBuilder =>
{
    appBuilder.Use(async (context, next) =>
    {
        context.Features.Get<IHttpMaxRequestBodySizeFeature>().MaxRequestBodySize = 300_000_000;
        await next.Invoke();
    });
});

app.UseRouting();

app.UseCors();

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();

app.UseEndpoints(endpoints =>
{
    /*endpoints.MapControllerRoute(name: "tgwebhook",
             pattern: $"bot/{botToken}",
             new { controller = "Webhook", action = "Post" });*/
    endpoints.MapControllers().RequireCors("enablecorspolicy");
});

app.Run();
