using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SS_Microservice.Common.Services.CurrentUser;

namespace SS_Microservice.Services.Address.Infrastructure.Data.DBContext
{
    public class AddressDbContextFactory : IDesignTimeDbContextFactory<AddressDbContext>
    {
        private readonly ICurrentUserService _currentService;

        public AddressDbContextFactory()
        {
        }

        public AddressDbContextFactory(ICurrentUserService currentService)
        {
            _currentService = currentService;
        }

        public AddressDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("AddressDbContext");

            var optionBuilder = new DbContextOptionsBuilder<AddressDbContext>();
            optionBuilder.UseMySQL(connectionString);

            return new AddressDbContext(optionBuilder.Options, _currentService);
        }
    }
}