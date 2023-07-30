using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace SS_Microservice.Services.Auth.Infrastructure.Data.DBContext
{
    public class DBContextFactorty : IDesignTimeDbContextFactory<DBContext>
    {
        public DBContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AppDbContext");

            var optionBuilder = new DbContextOptionsBuilder<DBContext>();
            optionBuilder.UseSqlServer(connectionString);

            return new DBContext(optionBuilder.Options);
        }
    }
}