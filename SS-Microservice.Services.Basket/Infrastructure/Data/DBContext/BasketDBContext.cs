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
        private ICurrentUserService _currentUserService;

        public ICurrentUserService CurrentUserService
        {
            set
            {
                this._currentUserService = value;
            }
        }

        public BasketDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BasketConfiguration());
            builder.ApplyConfiguration(new BasketItemConfiguration());
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (EntityEntry<BaseAuditableEntity<int>> entry in ChangeTracker.Entries<BaseAuditableEntity<int>>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = _currentUserService?.UserId ?? "System";
                        entry.Entity.CreatedDate = DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.UpdatedBy = _currentUserService?.UserId ?? "System";
                        entry.Entity.UpdatedDate = DateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        public DbSet<Domain.Entities.Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}