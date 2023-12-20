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
using SS_Microservice.Common.Types.Enums;
using SS_Microservice.Common.Validators;
using SS_Microservice.Services.Inventory.Application.Common.AutoMapper;
using SS_Microservice.Services.Inventory.Application.Interfaces;
using SS_Microservice.Services.Inventory.Application.Services;
using SS_Microservice.Services.Inventory.Infrastructure.Consumers.Commands.OrderingSaga;
using SS_Microservice.Services.Inventory.Infrastructure.Consumers.Events.Order;
using SS_Microservice.Services.Inventory.Infrastructure.Data.DBContext;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Host
    .UseLogging()
    .UseAppMetrics(configuration);

builder.Services.AddMetrics();

builder.Services.AddProblemDetailsSetup();
builder.Services.AddDbContext<InventoryDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("InventoryDbContext")));

builder.Services
    .AddControllers()
    .ConfigureValidationErrorResponse();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services
            .AddSingleton<ICurrentUserService, CurrentUserService>()
            .AddScoped<IInventoryService, InventoryService>()
            .AddScoped(typeof(IUnitOfWork), typeof(SS_Microservice.Services.Inventory.Infrastructure.Repositories.UnitOfWork))
            .AddScoped(typeof(IGenericRepository<>), typeof(SS_Microservice.Services.Inventory.Infrastructure.Repositories.GenericRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

builder.Services.AddOpenTracing();

builder.Services.AddJaeger(configuration.GetJaegerOptions());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenWithJWTAuth();

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddMessaging(configuration, new List<EventBusConsumer>()
{
    {
        new EventBusConsumer()
        {
            Type = typeof(ExportInventoryCommandConsumer),
            Endpoint = EventBusConstant.ExportInventory
        }
    },
    {
        new EventBusConsumer()
        {
            Type = typeof(RollBackInventoryCommandConsumer),
            Endpoint = EventBusConstant.RollBackInventory
        }
    },
    {
        new EventBusConsumer()
        {
            Type = typeof(OrderCancelledEventConsumer),
            Endpoint = APPLICATION_SERVICE.INVENTORY_SERVICE + "_" + EventBusConstant.OrderCancelled
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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase<InventoryDbContext>();
app.Run();