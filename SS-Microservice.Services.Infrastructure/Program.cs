using AutoMapper;
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
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Common.Validators;
using SS_Microservice.Services.Infrastructure.Application.Common.SignalR;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Application.Services;
using SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Commands.Mail;
using SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Events.Order;
using SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Events.OrderingSaga;
using SS_Microservice.Services.Infrastructure.Infrastructure.Consumers.Events.User;
using SS_Microservice.Services.Infrastructure.Infrastructure.Data.DBContext;
using SS_Microservice.Services.Infrastructure.Infrastructure.Repositories;
using SS_Microservice.Services.Infrastructure.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
// Add services to the container.
var configuration = builder.Configuration;

builder.Host
    .UseLogging();
//.UseAppMetrics(configuration);

builder.WebHost.UseKestrel(options =>
{
    options.ListenAnyIP(5007, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});

//builder.Services.AddMetrics();

builder.Services.AddProblemDetailsSetup();
builder.Services.AddDbContext<InfrastructureDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("InfrastructureDbContext")));
builder.Services
    .AddControllers()
    .ConfigureValidationErrorResponse();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddMaps(Assembly.GetEntryAssembly());
}).CreateMapper());

builder.Services
            .AddSingleton<ICurrentUserService, CurrentUserService>()
            .AddTransient<IMailService, MailService>()
            .AddTransient<INotificationService, NotificationService>()
            .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddMessaging(configuration,
    new List<EventBusConsumer>()
    {
        {
            new EventBusConsumer()
            {
                Type = typeof(SendMailCommandConsumer),
                Endpoint = EventBusConstant.SendMail
            }
        },
        {
            new EventBusConsumer()
            {
                Type = typeof(OrderCreationCompletedEventConsumer),
                Endpoint = APPLICATION_SERVICE.INFRASTRUCTURE_SERVICE + "_" + EventBusConstant.OrderCreationCompleted
            }
        },
        {
            new EventBusConsumer()
            {
                Type = typeof(OrderCreationRejectedEventConsumer),
                Endpoint = APPLICATION_SERVICE.INFRASTRUCTURE_SERVICE + "_" + EventBusConstant.OrderCreationRejected
            }
        },
        {
            new EventBusConsumer()
            {
                Type = typeof(OrderPaypalCompletedEventConsumer),
                Endpoint = APPLICATION_SERVICE.INFRASTRUCTURE_SERVICE + "_" + EventBusConstant.OrderPaypalCompleted
            }
        },
        {
            new EventBusConsumer()
            {
                Type = typeof(OrderStatusUpdatedEventConsumer),
                Endpoint = APPLICATION_SERVICE.INFRASTRUCTURE_SERVICE + "_" + EventBusConstant.OrderStatusUpdated
            }
        },
        {
            new EventBusConsumer()
            {
                Type = typeof(UserRegistedEventConsumer),
                Endpoint = APPLICATION_SERVICE.INFRASTRUCTURE_SERVICE + "_" + EventBusConstant.UserRegisted
            }
        }
    }
);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

//builder.Services.AddOpenTracing();

//builder.Services.AddJaeger(configuration.GetJaegerOptions());

builder.Services.AddCustomOpenTelemetry(configuration);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenWithJWTAuth();

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddConsul(builder.Configuration.GetConsulConfig());

builder.Services.AddAuthorization();

builder.Services.AddSignalR();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseProblemDetails();
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
//app.UseHttpsRedirection();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<InfrastructureHub>("/infrastructure-hub");
app.MigrateDatabase<InfrastructureDbContext>();
app.Run();