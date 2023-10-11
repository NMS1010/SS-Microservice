using AutoMapper;
using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Common.Jaeger;
using SS_Microservice.Common.Jwt;
using SS_Microservice.Common.Logging;
using SS_Microservice.Common.Middleware;
using SS_Microservice.Common.Migration;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Common.Validators;
using SS_Microservice.Services.Order.Application.Common.AutoMapper;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Infrastructure.Data.DBContext;
using SS_Microservice.Services.Order.Infrastructure.Repositories;
using SS_Microservice.Services.Order.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Host.UseLogging();
builder.Services.AddProblemDetailsSetup();
var configuration = builder.Configuration;
builder.Services.AddDbContext<OrderDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("OrderDbContext")));
builder.Services.AddControllers()
    .ConfigureValidationErrorResponse();
builder.Services.AddHttpContextAccessor();
builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new OrderProfile(provider.GetService<IHttpContextAccessor>()));
}).CreateMapper());
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Description = @"JWT authorization header using the Bearer sheme. \r\n\r\n
                        Enter 'Bearer' [space] and then your token in the text input below.
                        \r\n\r\nExample: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    s.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme()
            {
                Reference = new OpenApiReference()
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>
            (o => o.Address = new Uri(configuration["GrpcSettings:ProductUrl"]));
builder.Services
    .AddScoped<IProductGrpcService, ProductGrpcService>()
    .AddSingleton<ICurrentUserService, CurrentUserService>()
    .AddScoped<IUploadService, UploadService>()
    .AddScoped<IOrderService, OrderService>()
    .AddScoped<IOrderStateService, OrderStateService>()
    .AddScoped<IDeliveryService, DeliveryService>()
    .AddScoped<IPaymentMethodService, PaymentMethodService>()
    .AddScoped<IOrderCancellationReasonService, OrderCancellationReasonService>()
    .AddScoped<ITransactionService, TransactionService>()
    .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
    .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddOpenTracing();
builder.Services.AddJaeger(configuration.GetJaegerOptions());
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
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase<OrderDbContext>();

app.Run();