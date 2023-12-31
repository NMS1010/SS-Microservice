using Hellang.Middleware.ProblemDetails;
using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Jwt;
using SS_Microservice.Common.Logging;
using SS_Microservice.Common.Middleware;
using SS_Microservice.Common.OpenTelemetry;
using SS_Microservice.Common.Swagger;
using SS_Microservice.Common.Validators;

var builder = WebApplication.CreateBuilder(args);

builder.Host
    .UseLogging();
//.UseMetrics();

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5201, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});

builder.Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

//builder.Services.AddMetrics();
// Add services to the container.
var routes = "Routes";

var configuration = builder.Configuration;

// specific multiple ocelot file in Routes folder
builder.Configuration.AddOcelotWithSwaggerSupport(options =>
{
    options.Folder = routes;
});

//add ocelot with consul
builder.Services.AddOcelot(builder.Configuration)
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    })
    .AddPolly()
    .AddConsul()
    .AddConfigStoredInConsul();

//builder.Services.AddOpenTracing();

//builder.Services.AddJaeger(builder.Configuration.GetJaegerOptions());

builder.Services.AddCustomOpenTelemetry(configuration);

builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddProblemDetailsSetup();

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddOcelot(routes, builder.Environment)
    .AddEnvironmentVariables();

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddConsul(builder.Configuration.GetConsulConfig());

builder.Services.AddSwaggerGenWithJWTAuth();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseProblemDetails();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<InternalAPIMiddleware>();
//app.UseHttpsRedirection();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.UseCors();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseWebSockets();
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
    options.ReConfigureUpstreamSwaggerJson = SS_Microservice.APIGateway.Configs.AlterUpstream.AlterUpstreamSwaggerJson;
}).UseOcelot().Wait();

app.Run();