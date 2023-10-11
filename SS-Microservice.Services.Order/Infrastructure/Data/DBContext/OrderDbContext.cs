using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SS_Microservice.Common.Entities;
using SS_Microservice.Common.Services.CurrentUser;
using SS_Microservice.Services.Order.Domain.Entities;
using SS_Microservice.Services.Order.Infrastructure.Data.Configurations;

namespace SS_Microservice.Services.Order.Infrastructure.Data.DBContext
{
    public class OrderDbContext : DbContext
    {
        private readonly ICurrentUserService _currentUserService;

        public OrderDbContext(DbContextOptions options, ICurrentUserService currentUserService) : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(OrderConfiguration).Assembly);
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

        public DbSet<Domain.Entities.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
        public DbSet<Delivery> Deliveries { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<PaymentMethod> PaymentMethods { get; set; }
        public DbSet<OrderCancellationReason> OrderCancellationReasons { get; set; }
    }
}