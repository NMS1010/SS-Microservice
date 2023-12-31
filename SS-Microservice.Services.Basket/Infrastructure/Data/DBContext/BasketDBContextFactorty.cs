﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SS_Microservice.Common.Services.CurrentUser;

namespace SS_Microservice.Services.Basket.Infrastructure.Data.DBContext
{
    public class BasketDBContextFactorty : IDesignTimeDbContextFactory<BasketDBContext>
    {
        private readonly ICurrentUserService _currentService;

        public BasketDBContextFactorty()
        {
        }

        public BasketDBContextFactorty(ICurrentUserService currentService)
        {
            _currentService = currentService;
        }

        public BasketDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("BasketDbContext");

            var optionBuilder = new DbContextOptionsBuilder<BasketDBContext>();
            optionBuilder.UseMySQL(connectionString);

            return new BasketDBContext(optionBuilder.Options, _currentService);
        }
    }
}