using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace SS_Microservice.Services.Auth.Infrastructure.Data.DBContext
{
    public class AuthDbContextFactorty : IDesignTimeDbContextFactory<AuthDbContext>
    {
        public AuthDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AuthDbContext");

            var optionBuilder = new DbContextOptionsBuilder<AuthDbContext>();
            optionBuilder.UseMySQL(connectionString);

            return new AuthDbContext(optionBuilder.Options);
        }
    }
}