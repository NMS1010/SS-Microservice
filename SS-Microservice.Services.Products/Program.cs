using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Jaeger;
using SS_Microservice.Common.Jwt;
using SS_Microservice.Common.Middleware;
using SS_Microservice.Common.RabbitMQ;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Services.Upload;
using SS_Microservice.Services.Categorys.Infrastructure.Repositories;
using SS_Microservice.Services.Products.Application.Common.AutoMapper;
using SS_Microservice.Services.Products.Application.Features.Order.EventConsumer;
using SS_Microservice.Services.Products.Application.Interfaces;
using SS_Microservice.Services.Products.Application.Interfaces.Repositories;
using SS_Microservice.Services.Products.Infrastructure.Data.Context;
using SS_Microservice.Services.Products.Infrastructure.Repositories;
using SS_Microservice.Services.Products.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
// Add services to the container.
builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Services.Configure<MongoDBSettings>(
               configuration.GetSection("ProductDatabaseSetting"));

builder.Services.AddSingleton<IMongoDBSettings>(sp =>
    sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);
builder.Services.AddHttpContextAccessor();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddSingleton(provider => new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MapperProfile(provider.GetService<IHttpContextAccessor>()));
}).CreateMapper());
builder.Services
    .AddSingleton<ICurrentUserService, CurrentUserService>()
    .AddScoped<IProductContext, ProductContext>()
    .AddScoped<IUploadService, UploadService>()
    .AddScoped<IProductRepository, ProductRepository>()
    .AddScoped<IProductService, ProductService>()
    .AddScoped<IBrandRepository, BrandRepository>()
    .AddScoped<IBrandService, BrandService>()
    .AddScoped<IProductVariantService, ProductVariantService>()
    .AddScoped<ICategoryRepository, CategoryRepository>()
    .AddScoped<ICategoryService, CategoryService>();

builder.Services.AddGrpc();
builder.Services.AddControllers();
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
builder.Services.AddConsul(configuration.GetConsulConfig());
builder.Services.AddOpenTracing();
builder.Services.AddJaeger(configuration.GetJaegerOptions());
builder.Services.AddMessaging(configuration, new List<Type>()
{
    {
        typeof(OrderCreatedConsumer)
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

app.Run();