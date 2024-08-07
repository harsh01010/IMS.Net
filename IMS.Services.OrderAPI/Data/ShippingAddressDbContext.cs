using IMS.Services.OrderAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMS.Services.OrderAPI.Data
{
    public class ShippingAddressDbContext:DbContext
    {
        public ShippingAddressDbContext(DbContextOptions<ShippingAddressDbContext> options) : base(options)
        {
        }

       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ShippingAddress>().HasKey(x => x.shippingAddressId);
        }

      public  DbSet<ShippingAddress> ShippingAddresses { get; set; }
    }
}
