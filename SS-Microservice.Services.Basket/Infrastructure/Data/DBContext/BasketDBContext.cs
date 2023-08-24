using Microsoft.EntityFrameworkCore;
using SS_Microservice.Services.Basket.Core.Entities;
using SS_Microservice.Services.Basket.Infrastructure.Data.Configurations;

namespace SS_Microservice.Services.Basket.Infrastructure.Data.DBContext
{
    public class BasketDBContext : DbContext
    {
        public BasketDBContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new BasketConfiguration());
            builder.ApplyConfiguration(new BasketItemConfiguration());
        }

        public DbSet<Core.Entities.Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
    }
}