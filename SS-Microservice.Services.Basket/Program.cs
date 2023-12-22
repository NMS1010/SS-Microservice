using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Grpc.Product.Protos;
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
using SS_Microservice.Services.Basket.Application.Common.AutoMapper;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Services;
using SS_Microservice.Services.Basket.Infrastructure.Consumers.Commands.OrderingSaga;
using SS_Microservice.Services.Basket.Infrastructure.Consumers.Events.User;
using SS_Microservice.Services.Basket.Infrastructure.Data.DBContext;
using SS_Microservice.Services.Basket.Infrastructure.Repositories;
using SS_Microservice.Services.Basket.Infrastructure.Services;
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
    options.ListenAnyIP(5216, listenOptions =>
    {
        listenOptions.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
    });
});

//builder.Services.AddMetrics();

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

builder.Services.AddMessaging(configuration, new List<EventBusConsumer>()
{
    {
        new EventBusConsumer()
        {
            Type = typeof(ClearBasketCommandConsumer),
            Endpoint = EventBusConstant.ClearBasket
        }
    },
    {
        new EventBusConsumer()
        {
            Type = typeof(UserRegistedConsumer),
            Endpoint = APPLICATION_SERVICE.BASKET_SERVICE + "_" + EventBusConstant.UserRegisted
        }
    }
});

//builder.Services.AddOpenTracing();

//builder.Services.AddJaeger(configuration.GetJaegerOptions());

builder.Services.AddCustomOpenTelemetry(configuration);

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
//app.UseHttpsRedirection();
app.UseOpenTelemetryPrometheusScrapingEndpoint();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase<BasketDBContext>();

app.Run();