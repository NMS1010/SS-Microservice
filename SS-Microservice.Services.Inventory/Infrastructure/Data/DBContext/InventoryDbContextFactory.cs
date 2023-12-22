using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SS_Microservice.Common.Services.CurrentUser;

namespace SS_Microservice.Services.Inventory.Infrastructure.Data.DBContext
{
    public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
    {
        private readonly ICurrentUserService _currentService;

        public InventoryDbContextFactory()
        {
        }

        public InventoryDbContextFactory(ICurrentUserService currentService)
        {
            _currentService = currentService;
        }

        public InventoryDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("InventoryDbContext");

            var optionBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
            optionBuilder.UseMySQL(connectionString);

            return new InventoryDbContext(optionBuilder.Options, _currentService);
        }
    }
}
