using InventoryTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }

        public DbSet<LocationModel> Locations { get; set; }
    }
}
