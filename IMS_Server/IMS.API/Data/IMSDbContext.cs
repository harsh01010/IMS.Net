using IMS.API.Models.Domain.Order;
using IMS.API.Models.Domain.Product;
using IMS.API.Models.Domain.ShippingAddress;
using IMS.API.Models.Domain.ShoppingCart;
using Microsoft.EntityFrameworkCore;

namespace IMS.API.Data
{
    public class IMSDbContext : DbContext
    {
        public IMSDbContext(DbContextOptions<IMSDbContext> options) : base(options)
        {

        }
        //Produts 
        public DbSet<ProductModel> Products { get; set; }

        //Carts
        public DbSet<CartModel> Carts { get; set; }
        public DbSet<CartProductModel> CartProducts { get; set; }

        //shipping address
        public DbSet<ShippingAddressModel> ShippingAddresses { get; set; }

        //orders
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<OrderItemModel> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductModel>().HasData(new ProductModel
            {
                ProductId = Guid.NewGuid(),
                Name = "Samosa",
                Price = 15,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/603x403",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel
            {
                ProductId = Guid.NewGuid(),
                Name = "Paneer Tikka",
                Price = 13.99,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/602x402",
                CategoryName = "Appetizer"
            });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel
            {
                ProductId = Guid.NewGuid(),
                Name = "Sweet Pie",
                Price = 10.99,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/601x401",
                CategoryName = "Dessert"
            });
            modelBuilder.Entity<ProductModel>().HasData(new ProductModel
            {
                ProductId = Guid.NewGuid(),
                Name = "Pav Bhaji",
                Price = 15,
                Description = " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.",
                ImageUrl = "https://placehold.co/600x400",
                CategoryName = "Entree"
            });


            modelBuilder.Entity<CartProductModel>()
          .HasKey(cp => new { cp.CartId, cp.ProductId });

            modelBuilder.Entity<CartProductModel>()
                .HasOne(cp => cp.Cart)
                .WithMany(c => c.CartProducts)
                .HasForeignKey(cp => cp.CartId);

            modelBuilder.Entity<ShippingAddressModel>().HasKey(x => x.shippingAddressId);


            modelBuilder.Entity<OrderItemModel>().HasKey(x => new { x.OrderId, x.ProductId });

            modelBuilder.Entity<OrderModel>().HasKey(x => x.OrderId);

            modelBuilder.Entity<OrderItemModel>()
                .HasOne(x => x.order)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.OrderId);


        }
    }
}
