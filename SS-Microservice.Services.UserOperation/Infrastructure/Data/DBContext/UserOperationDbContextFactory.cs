using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SS_Microservice.Common.Services.CurrentUser;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Data.DBContext
{
    public class UserOperationDbContextFactory : IDesignTimeDbContextFactory<UserOperationDbContext>
    {
        private readonly ICurrentUserService _currentService;

        public UserOperationDbContextFactory()
        {
        }

        public UserOperationDbContextFactory(ICurrentUserService currentService)
        {
            _currentService = currentService;
        }

        public UserOperationDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("UserOperationDbContext");

            var optionBuilder = new DbContextOptionsBuilder<UserOperationDbContext>();
            optionBuilder.UseMySQL(connectionString);

            return new UserOperationDbContext(optionBuilder.Options, _currentService);
        }
    }
}
