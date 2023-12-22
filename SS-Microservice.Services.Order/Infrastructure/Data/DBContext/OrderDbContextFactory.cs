﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SS_Microservice.Common.Services.CurrentUser;

namespace SS_Microservice.Services.Order.Infrastructure.Data.DBContext
{
    public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        private readonly ICurrentUserService _currentService;

        public OrderDbContextFactory()
        {
        }

        public OrderDbContextFactory(ICurrentUserService currentService)
        {
            _currentService = currentService;
        }

        public OrderDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("OrderDbContext");

            var optionBuilder = new DbContextOptionsBuilder<OrderDbContext>();
            optionBuilder.UseMySQL(connectionString);

            return new OrderDbContext(optionBuilder.Options, _currentService);
        }
    }
}