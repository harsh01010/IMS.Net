using IMS.Services.ShoppingCartAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace IMS.Services.OrderAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<CartDbContext> options) : base(options)
        {
            
        }

    }
}
