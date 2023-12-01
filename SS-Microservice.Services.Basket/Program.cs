using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Grpc.Product.Protos;
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
using SS_Microservice.Services.Basket.Application.Common.AutoMapper;
using SS_Microservice.Services.Basket.Application.Features.Product.EventConsumer;
using SS_Microservice.Services.Basket.Application.Features.User.EventConsumer;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Infrastructure.Data.DBContext;
using SS_Microservice.Services.Basket.Infrastructure.Repositories;
using SS_Microservice.Services.Basket.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Host
    .UseLogging()
    .UseAppMetrics(configuration);

builder.Services.AddMetrics();

builder.Services.AddProblemDetailsSetup();
builder.Services.AddDbContext<BasketDBContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("BasketDbContext")));
builder.Services.AddControllers()
    .ConfigureValidationErrorResponse();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(BasketProfile).Assembly);

builder.Services
            .AddSingleton<ICurrentUserService, CurrentUserService>()
            .AddScoped<IBasketService, BasketService>()
            .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

#pragma warning disable CS0612 // Type or member is obsolete
builder.Services.AddMessaging(configuration, typeof(UserRegistedConsumer), typeof(ProductInventoryUpdatedConsumer));
#pragma warning restore CS0612 // Type or member is obsolete

builder.Services.AddOpenTracing();
builder.Services.AddJaeger(configuration.GetJaegerOptions());
// Grpc Configuration
builder.Services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>
            (o => o.Address = new Uri(configuration["GrpcSettings:ProductUrl"]));
builder.Services.AddScoped<IProductGrpcService, ProductGrpcService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithJWTAuth();
builder.Services.AddJwtAuthentication(configuration);
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
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase<BasketDBContext>();

app.Run();