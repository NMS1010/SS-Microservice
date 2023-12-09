using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SS_Microservice.Common.Services.CurrentUser;

namespace SS_Microservice.Services.Products.Infrastructure.Data.Context
{
    public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
    {
        private readonly ICurrentUserService _currentService;

        public ProductDbContextFactory()
        {
        }

        public ProductDbContextFactory(ICurrentUserService currentService)
        {
            _currentService = currentService;
        }

        public ProductDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("ProductDbContext");

            var optionBuilder = new DbContextOptionsBuilder<ProductDbContext>();
            optionBuilder.UseMySQL(connectionString);

            return new ProductDbContext(optionBuilder.Options, _currentService);
        }
    }
}