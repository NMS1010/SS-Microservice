using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SS_Microservice.Common.Services.CurrentUser;

namespace SS_Microservice.Services.Infrastructure.Infrastructure.Data.DBContext
{
    public class InfrastructureDbContextFactory : IDesignTimeDbContextFactory<InfrastructureDbContext>
    {
        private readonly ICurrentUserService _currentService;

        public InfrastructureDbContextFactory()
        {
        }

        public InfrastructureDbContextFactory(ICurrentUserService currentService)
        {
            _currentService = currentService;
        }

        public InfrastructureDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: true, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("InfrastructureDbContext");

            var optionBuilder = new DbContextOptionsBuilder<InfrastructureDbContext>();
            optionBuilder.UseMySQL(connectionString);

            return new InfrastructureDbContext(optionBuilder.Options, _currentService);
        }
    }
}