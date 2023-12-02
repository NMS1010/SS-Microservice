using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SS_Microservice.Common.Entities;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.UserOperation.Domain.Entities;
using SS_Microservice.Services.UserOperation.Infrastructure.Data.Configurations;

namespace SS_Microservice.Services.UserOperation.Infrastructure.Data.DBContext
{
    public class UserOperationDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public UserOperationDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ReviewConfiguration).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<BaseAuditableEntity<long>> entry in ChangeTracker.Entries<BaseAuditableEntity<long>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService?.UserId ?? "System";
                        entry.Entity.CreatedAt = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService?.UserId ?? "System";
                        entry.Entity.UpdatedAt = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserFollowProduct> UserFollowProducts { get; set; }

    }
}
