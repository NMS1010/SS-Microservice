using Hellang.Middleware.ProblemDetails;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Jaeger;
using SS_Microservice.Common.Jwt;
using SS_Microservice.Common.Logging;
using SS_Microservice.Common.Metrics;
using SS_Microservice.Common.Middleware;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Swagger;
using SS_Microservice.Common.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var configuration = builder.Configuration;

builder.Host
    .UseLogging()
    .UseAppMetrics(configuration);

builder.Services.AddMetrics();

builder.Services.AddProblemDetailsSetup();

builder.Services
    .AddControllers()
    .ConfigureValidationErrorResponse();

builder.Services.AddHttpContextAccessor();
builder.Services.AddOpenTracing();

builder.Services.AddJaeger(configuration.GetJaegerOptions());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGenWithJWTAuth();

builder.Services.AddJwtAuthentication(configuration);

builder.Services.AddMessaging(configuration);

builder.Services.AddConsul(builder.Configuration.GetConsulConfig());


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

app.MapControllers();
app.Run();