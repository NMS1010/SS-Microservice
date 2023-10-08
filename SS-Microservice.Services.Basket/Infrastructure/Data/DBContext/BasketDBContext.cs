using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SS_Microservice.Common.Entities;
using SS_Microservice.Common.Entities.Intefaces;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Basket.Domain.Entities;
using SS_Microservice.Services.Basket.Infrastructure.Data.Configurations;

namespace SS_Microservice.Services.Basket.Infrastructure.Data.DBContext
{
    public class BasketDBContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public BasketDBContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BasketConfiguration());
            builder.ApplyConfiguration(new BasketItemConfiguration());
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

        public DbSet<Domain.Entities.Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}