using IMS.Services.ShoppingCartAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace IMS.Services.ShoppingCartAPI.Data
{
    public class CartDbContext:DbContext
    {
       
        public CartDbContext(DbContextOptions<CartDbContext> options) : base(options)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CartProduct>()
           .HasKey(cp => new { cp.CartId, cp.ProductId });

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Cart)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(cp => cp.CartId);
        }

        
    }
}
