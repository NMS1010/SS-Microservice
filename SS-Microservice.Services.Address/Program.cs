using FluentValidation;
using Hellang.Middleware.ProblemDetails;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Serilog;
using SS_Microservice.Common.Consul;
using SS_Microservice.Common.Jaeger;
using SS_Microservice.Common.Jwt;
using SS_Microservice.Common.Logging;
using SS_Microservice.Common.Middleware;
using SS_Microservice.Common.Migration;
using SS_Microservice.Common.Model.CustomResponse;
using SS_Microservice.Common.Repository;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Common.Validators;
using SS_Microservice.Services.Address.Application.Common.AutoMapper;
using SS_Microservice.Services.Address.Application.Interfaces;
using SS_Microservice.Services.Address.Infrastructure.Data.DBContext;
using SS_Microservice.Services.Address.Infrastructure.Repositories;
using SS_Microservice.Services.Address.Infrastructure.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
builder.Host.UseLogging();

var configuration = builder.Configuration;
builder.Services.AddProblemDetailsSetup();
builder.Services.AddDbContext<AddressDbContext>(options =>
                options.UseMySQL(configuration.GetConnectionString("AddressDbContext")));
builder.Services
    .AddControllers()
    .ConfigureValidationErrorResponse();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(AddressProfile).Assembly);

builder.Services
            .AddSingleton<ICurrentUserService, CurrentUserService>()
            .AddScoped<IAddressService, AddressService>()
            .AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork))
            .AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
builder.Services.AddOpenTracing();
builder.Services.AddJaeger(configuration.GetJaegerOptions());
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
app.UseProblemDetails();
app.UseSerilogRequestLogging();
app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MigrateDatabase<AddressDbContext>();
app.Run();