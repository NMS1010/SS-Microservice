using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Jaeger;
using SS_Microservice.Common.Logging;
using SS_Microservice.Common.Metrics;
using SS_Microservice.Common.Middleware;
using SS_Microservice.Common.Migration;
using SS_Microservice.Common.Model.CustomResponse;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Common.Validators;
using SS_Microservice.Services.Auth.Application.Common.AutoMapper;
using SS_Microservice.Services.Auth.Application.Common.Constants;
using SS_Microservice.Services.Auth.Application.Interfaces;
using SS_Microservice.Services.Auth.Domain.Entities;
using SS_Microservice.Services.Auth.Infrastructure.Data.DBContext;
using SS_Microservice.Services.Auth.Infrastructure.Repositotires;
using SS_Microservice.Services.Auth.Infrastructure.Services;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;

builder.Host
    .UseLogging()
    .UseAppMetrics(configuration);

builder.Services.AddMetrics();

builder.Services.AddProblemDetailsSetup();
// Add services to the container.
builder.Services.AddDbContext<AuthDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("AuthDbContext")));

builder.Services.AddIdentity<AppUser, AppRole>(opts =>
{
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequiredLength = 5;
    opts.Password.RequireDigit = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
})
    .AddEntityFrameworkStores<AuthDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddMessaging(configuration);
builder.Services.AddOpenTracing();
builder.Services.AddJaeger(configuration.GetJaegerOptions());
builder.Services.AddControllers()
    .ConfigureValidationErrorResponse();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConsul(builder.Configuration.GetConsulConfig());
builder.Services
        .AddScoped<IJwtService, JwtService>()
        .AddScoped<IAuthService, AuthService>()
        .AddScoped<IUserService, UserService>()
        .AddScoped<IUploadService, UploadService>()
        .AddSingleton<ICurrentUserService, CurrentUserService>()
        .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
        .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

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
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MigrateDatabase<AuthDbContext>();
app.Run();