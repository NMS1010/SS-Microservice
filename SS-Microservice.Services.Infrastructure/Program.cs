using AutoMapper;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Jaeger;
using SS_Microservice.Common.Jwt;
using SS_Microservice.Common.Logging;
using SS_Microservice.Common.Metrics;
using SS_Microservice.Common.Middleware;
using SS_Microservice.Common.Migration;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Swagger;
using SS_Microservice.Common.Validators;
using SS_Microservice.Services.Infrastructure.Application.Common.SignalR;
using SS_Microservice.Services.Infrastructure.Application.Interfaces;
using SS_Microservice.Services.Infrastructure.Application.Services;
using SS_Microservice.Services.Infrastructure.Infrastructure.Data.DBContext;
using SS_Microservice.Services.Infrastructure.Infrastructure.Repositories;
using SS_Microservice.Services.Infrastructure.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Host
    .UseLogging()
    .UseAppMetrics(configuration);

builder.Services.AddMetrics();

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

builder.Services.AddMessaging(configuration);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddOpenTracing();

builder.Services.AddJaeger(configuration.GetJaegerOptions());

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
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapHub<InfrastructureHub>("/infrastructure-hub");
app.MigrateDatabase<InfrastructureDbContext>();
app.Run();