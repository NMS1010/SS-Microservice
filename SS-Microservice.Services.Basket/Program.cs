using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Grpc.Product.Protos;
using SS_Microservice.Common.Jwt;
using SS_Microservice.Common.Middleware;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Basket.Application.Common.AutoMapper;
using SS_Microservice.Services.Basket.Infrastructure.Data.DBContext;
using SS_Microservice.Services.Basket.Infrastructure.Repositories;
using SS_Microservice.Services.Basket.Infrastructure.Services;
using System.Reflection;
using SS_Microservice.Common.Jaeger;
using SS_Microservice.Services.Basket.Application.Interfaces;
using SS_Microservice.Services.Basket.Application.Features.User.EventConsumer;
using SS_Microservice.Common.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
var configuration = builder.Configuration;
builder.Services.AddDbContext<BasketDBContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("BasketDbContext")));
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(BasketProfile).Assembly);

builder.Services
            .AddSingleton<ICurrentUserService, CurrentUserService>()
            .AddScoped<IBasketService, BasketService>()
            .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddMessaging(configuration, new List<Type>()
{
    {
        typeof(UserRegistedConsumer)
    }
});

builder.Services.AddOpenTracing();
builder.Services.AddJaeger(configuration.GetJaegerOptions());
// Grpc Configuration
builder.Services.AddGrpcClient<ProductProtoService.ProductProtoServiceClient>
            (o => o.Address = new Uri(configuration["GrpcSettings:ProductUrl"]));
builder.Services.AddScoped<IProductGrpcService, ProductGrpcService>();

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

app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();