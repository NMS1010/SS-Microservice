using MassTransit;
using MassTransit.EntityFrameworkCoreIntegration;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SS_Microservice.Common.Configuration;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Jaeger;
using SS_Microservice.Common.Logging;
using SS_Microservice.Common.Metrics;
using SS_Microservice.Common.Middleware;
using SS_Microservice.Common.Migration;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Swagger;
using SS_Microservice.SagaOrchestration.DbContext;
using SS_Microservice.SagaOrchestration.StateInstances.Ordering;
using SS_Microservice.SagaOrchestration.StateMachines.Ordering;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Host
    .UseLogging()
    .UseAppMetrics(configuration);

builder.Services.AddMetrics();

builder.Services
    .AddControllers();

builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenTracing();

builder.Services.AddJaeger(configuration.GetJaegerOptions());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenWithJWTAuth();

builder.Services.AddDbContext<SagaAppDBContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("SagaDBContext")));

var rabbitMqSettings = configuration.GetOptions<RabbitMqSettings>("RabbitMqSettings");
builder.Services
    .AddMassTransit(mt =>
    {

        mt.SetKebabCaseEndpointNameFormatter();

        var entryAssembly = Assembly.GetEntryAssembly();
        mt.AddConsumers(entryAssembly);
        mt.AddSagaStateMachine<OrderingStateMachine, OrderingStateInstance>()
        .EntityFrameworkRepository(r =>
        {
            r.ConcurrencyMode = ConcurrencyMode.Pessimistic; // or use Optimistic, which requires RowVersion
            r.LockStatementProvider = new MySqlLockStatementProvider();
            r.CustomizeQuery(x => x.Include(y => y.ProductInstances));
            r.AddDbContext<DbContext, SagaAppDBContext>((provider, builder) =>
            {
                builder.UseMySQL(configuration.GetConnectionString("SagaDBContext"), m =>
                {
                    m.MigrationsAssembly(Assembly.GetExecutingAssembly().GetName().Name);
                    m.MigrationsHistoryTable($"__{nameof(SagaAppDBContext)}");
                });
            });
        });


        mt.UsingRabbitMq((context, cfg) =>
        {
            cfg.Host(rabbitMqSettings.Uri, "/", h =>
            {
                h.Username(rabbitMqSettings.UserName);
                h.Password(rabbitMqSettings.Password);
            });
            cfg.UseInMemoryOutbox();
            cfg.ReceiveEndpoint(EventBusConstant.OrderCreated, e =>
            {
                e.ConfigureSaga<OrderingStateInstance>(context);
            });
        });
    });
builder.Services.AddConsul(builder.Configuration.GetConsulConfig());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
//app.UseHttpsRedirection();
app.MapControllers();
app.MigrateDatabase<SagaAppDBContext>();
app.Run();