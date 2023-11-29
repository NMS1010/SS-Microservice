using MMLib.SwaggerForOcelot.DependencyInjection;
using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using Ocelot.Provider.Polly;
using Serilog;
using SS_Microservice.Common.Jwt;
using SS_Microservice.Common.Logging;
using SS_Microservice.Common.Swagger;

var builder = WebApplication.CreateBuilder(args);

//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Host.UseLogging();
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
    .AddConsul()
    .AddCacheManager(x =>
    {
        x.WithDictionaryHandle();
    })
    .AddPolly();

builder.Services.AddSwaggerForOcelot(builder.Configuration);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddOcelot(routes, builder.Environment)
    .AddEnvironmentVariables();


builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddAuthorization();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenWithJWTAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
}

app.UseSerilogRequestLogging();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSwaggerForOcelotUI(options =>
{
    options.PathToSwaggerGenerator = "/swagger/docs";
    options.ReConfigureUpstreamSwaggerJson = SS_Microservice.APIGateway.Configs.AlterUpstream.AlterUpstreamSwaggerJson;
}).UseOcelot().Wait();

app.MapControllers();

app.Run();