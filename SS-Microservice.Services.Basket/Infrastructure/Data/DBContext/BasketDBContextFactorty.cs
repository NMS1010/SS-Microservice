using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace SS_Microservice.Services.Basket.Infrastructure.Data.DBContext
{
    public class BasketDBContextFactorty : IDesignTimeDbContextFactory<BasketDBContext>
    {
        public BasketDBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("BasketDbContext");

            var optionBuilder = new DbContextOptionsBuilder<BasketDBContext>();
            optionBuilder.UseMySQL(connectionString);

            return new BasketDBContext(optionBuilder.Options);
        }
    }
}