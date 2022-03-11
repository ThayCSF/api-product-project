using Microsoft.EntityFrameworkCore;
using Product.API.Project.Entities;

namespace Product.API.Project.Data
{
    public class ProductContext : DbContext
    {
        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Products> Products { get; set; } = null!;

    }
}
