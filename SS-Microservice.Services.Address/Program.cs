using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Jwt;
using SS_Microservice.Common.Logging;
using SS_Microservice.Common.Middleware;
using SS_Microservice.Common.Migration;
using SS_Microservice.Common.OpenTelemetry;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Swagger;
using SS_Microservice.Common.Validators;
using SS_Microservice.Services.Address.Application.Common.AutoMapper;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Application.Services;
using SS_Microservice.Services.Address.Infrastructure.Consumers.Commands.Auth;
using SS_Microservice.Services.Address.Infrastructure.Data.DBContext;
using SS_Microservice.Services.Address.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
var configuration = builder.Configuration;

builder.Host
    .UseLogging();
//.UseAppMetrics(configuration);

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5254, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});

//builder.Services.AddMetrics();

builder.Services.AddProblemDetailsSetup();
builder.Services.AddDbContext<AddressDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("AddressDbContext")));

builder.Services
    .AddControllers()
    .ConfigureValidationErrorResponse();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(AddressProfile).Assembly);

builder.Services
            .AddSingleton<ICurrentUserService, CurrentUserService>()
            .AddScoped<IAddressService, AddressService>()
            .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//builder.Services.AddOpenTracing();

//builder.Services.AddJaeger(configuration.GetJaegerOptions());

builder.Services.AddCustomOpenTelemetry(configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenWithJWTAuth();

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddMessaging(configuration, new List<EventBusConsumer>()
{
    {
        new EventBusConsumer()
        {
            Type = typeof(CreateAddressCommandConsumer),
            Endpoint = EventBusConstant.CreateAddress
        }
    },
    {
        new EventBusConsumer()
        {
            Type = typeof(UpdateAddressCommandConsumer),
            Endpoint = EventBusConstant.UpdateAddress
        }
    }
});

builder.Services.AddConsul(builder.Configuration.GetConsulConfig());

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseProblemDetails();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
//app.UseHttpsRedirection();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase<AddressDbContext>();
app.Run();