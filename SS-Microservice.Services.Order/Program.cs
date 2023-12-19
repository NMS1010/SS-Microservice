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
using SS_Microservice.Common.RestEase;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Common.Swagger;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Common.Validators;
using SS_Microservice.Services.Order.Application.Common.AutoMapper;
using SS_Microservice.Services.Order.Application.Interfaces;
using SS_Microservice.Services.Order.Application.Services;
using SS_Microservice.Services.Order.Infrastructure.Consumers.Events.OrderingSaga;
using SS_Microservice.Services.Order.Infrastructure.Data.DBContext;
using SS_Microservice.Services.Order.Infrastructure.Repositories;
using SS_Microservice.Services.Order.Infrastructure.Services;
using SS_Microservice.Services.Order.Infrastructure.Services.Address;
using SS_Microservice.Services.Order.Infrastructure.Services.Auth;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Host
    .UseLogging()
    .UseAppMetrics(configuration);

builder.Services.AddMetrics();

builder.Services.AddProblemDetailsSetup();

builder.Services.AddDbContext<OrderDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("OrderDbContext")));

builder.Services.AddControllers()
    .ConfigureValidationErrorResponse();

builder.Services.AddHttpContextAccessor();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenWithJWTAuth();

builder.Services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>
            (o => o.Address = new Uri(configuration["GrpcSettings:ProductUrl"]));

builder.Services.RegisterServiceForwarder<IAddressClientAPI>("address-service")
    .RegisterServiceForwarder<IAuthClientAPI>("auth-service");

builder.Services
    .AddScoped<IProductGrpcService, ProductGrpcService>()
    .AddSingleton<ICurrentUserService, CurrentUserService>()
    .AddScoped<IUploadService, UploadService>()
    .AddScoped<IOrderService, OrderService>()
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

builder.Services.AddMessaging(configuration,
    new List<EventBusConsumer>()
    {
        {
            new EventBusConsumer()
            {
                Type = typeof(OrderCreationCompletedEventConsumer),
                Endpoint = APPLICATION_SERVICE.ORDER_SERVICE + "_" + EventBusConstant.OrderCreationCompleted
            }
        },
        {
            new EventBusConsumer()
            {
                Type = typeof(OrderCreationRejectedEventConsumer),
                Endpoint = APPLICATION_SERVICE.ORDER_SERVICE + "_" + EventBusConstant.OrderCreationRejected
            }
        }
    });

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