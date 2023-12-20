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
using SS_Microservice.Common.RestEase;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Common.Swagger;
using SS_Microservice.Common.Validators;
using SS_Microservice.Services.UserOperation.Application.Common.AutoMapper;
using SS_Microservice.Services.UserOperation.Application.Interfaces;
using SS_Microservice.Services.UserOperation.Application.Services;
using SS_Microservice.Services.UserOperation.Infrastructure.Data.DBContext;
using SS_Microservice.Services.UserOperation.Infrastructure.Repositories;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Auth;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Order;
using SS_Microservice.Services.UserOperation.Infrastructure.Services.Product;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Host
    .UseLogging()
    .UseAppMetrics(configuration);

builder.Services.AddMetrics();

builder.Services.AddProblemDetailsSetup();
builder.Services.AddDbContext<UserOperationDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("UserOperationDbContext")));

builder.Services
    .AddControllers()
    .ConfigureValidationErrorResponse();

builder.Services.AddHttpContextAccessor();

builder.Services
    .RegisterServiceForwarder<IOrderClientAPI>("order-service")
    .RegisterServiceForwarder<IProductClientAPI>("products-service")
    .RegisterServiceForwarder<IAuthClientAPI>("auth-service");

builder.Services.AddAutoMapper(typeof(ReviewProfile).Assembly);

builder.Services
            .AddSingleton<ICurrentUserService, CurrentUserService>()
            .AddSingleton<IUploadService, UploadService>()
            .AddScoped<IReviewService, ReviewService>()
            .AddScoped<IUserFollowProductService, UserFollowProductService>()
            .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddOpenTracing();

builder.Services.AddJaeger(configuration.GetJaegerOptions());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenWithJWTAuth();

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddConsul(builder.Configuration.GetConsulConfig());

builder.Services.AddMessaging(configuration);

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
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase<UserOperationDbContext>();
app.Run();