using AutoMapper;
using FluentValidation;
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
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Common.Swagger;
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Common.Validators;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Services;
using SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.Inventory;
using SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.OrderingSaga;
using SS_Microservice.Services.Products.Infrastructure.Consumers.Commands.UserOperation;
using SS_Microservice.Services.Products.Infrastructure.Consumers.Events.Order;
using SS_Microservice.Services.Products.Infrastructure.Data.Context;
using SS_Microservice.Services.Products.Infrastructure.Repositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = builder.Configuration;

builder.Host
    .UseLogging()
    .UseAppMetrics(configuration);

builder.Services.AddMetrics();

builder.Services.AddProblemDetailsSetup();
builder.Services.AddDbContext<ProductDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("ProductDbContext")));

builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddMaps(Assembly.GetEntryAssembly());
}).CreateMapper());

builder.Services
    .AddSingleton<ICurrentUserService, CurrentUserService>()
    .AddScoped<IUploadService, UploadService>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IBrandService, BrandService>()
    .AddScoped<IUnitService, UnitService>()
    .AddScoped<IVariantService, VariantService>()
    .AddScoped<ISaleService, SaleService>()
    .AddScoped<IProductImageService, ProductImageService>()
    .AddScoped<ICategoryService, CategoryService>()
    .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
    .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddGrpc();
builder.Services.AddControllers()
    .ConfigureValidationErrorResponse();

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenWithJWTAuth();

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddConsul(configuration.GetConsulConfig());

builder.Services.AddOpenTracing();

builder.Services.AddJaeger(configuration.GetJaegerOptions());

builder.Services.AddMessaging(configuration, new List<EventBusConsumer>()
{
    {
        new EventBusConsumer()
        {
            Type = typeof(UpdateProductQuantityCommandConsumer),
            Endpoint = EventBusConstant.UpdateProductQuantity
        }
    },
    {
        new EventBusConsumer()
        {
            Type = typeof(ReserveStockCommandConsumer),
            Endpoint = EventBusConstant.ReserveStock
        }
    },
    {
        new EventBusConsumer()
        {
            Type = typeof(RollBackStockCommandConsumer),
            Endpoint = EventBusConstant.RollBackStock
        }
    },
    {
        new EventBusConsumer()
        {
            Type = typeof(OrderCancelledEventConsumer),
            Endpoint = APPLICATION_SERVICE.PRODUCT_SERVICE + "_" + EventBusConstant.OrderCancelled
        }
    },
    {
        new EventBusConsumer()
        {
            Type = typeof(UpdateProductRatingCommandConsumer),
            Endpoint = EventBusConstant.UpdateProductRating
        }
    }
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseRouting();
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<ProductService>();
});
app.MapControllers();
app.MigrateDatabase<ProductDbContext>();
app.Run();