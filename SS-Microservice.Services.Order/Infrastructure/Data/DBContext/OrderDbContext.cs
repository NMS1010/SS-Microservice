using Microsoft.EntityFrameworkCore;
using SS_Microservice.Services.Order.Core.Entities;
using SS_Microservice.Services.Order.Infrastructure.Data.Configurations;

namespace SS_Microservice.Services.Order.Infrastructure.Data.DBContext
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new OrderConfiguration());
            builder.ApplyConfiguration(new OrderItemConfiguration());
            builder.ApplyConfiguration(new OrderStateConfiguration());
        }

        public DbSet<Core.Entities.Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderState> OrderStates { get; set; }
    }
}