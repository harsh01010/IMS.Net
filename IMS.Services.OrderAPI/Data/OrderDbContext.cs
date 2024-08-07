using IMS.Services.OrderAPI.Models.Domain;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.EntityFrameworkCore;

namespace IMS.Services.OrderAPI.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }    
        public DbSet<OrderItem>OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OrderItem>().HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<Order>().HasKey(x => x.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(x=>x.order)
                .WithMany(x=>x.Items)
                .HasForeignKey(x=>x.OrderId);

        }


    }
}
