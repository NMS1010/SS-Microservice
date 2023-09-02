using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SS_Microservice.Services.Order.Infrastructure.Data.DBContext
{
    public class OrderDbContextFactory : IDesignTimeDbContextFactory<OrderDbContext>
    {
        public OrderDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("OrderDbContext");

            var optionBuilder = new DbContextOptionsBuilder<OrderDbContext>();
            optionBuilder.UseMySQL(connectionString);

            return new OrderDbContext(optionBuilder.Options);
        }
    }
}