using Digital_Menu.Models;
using Microsoft.EntityFrameworkCore;
namespace Digital_Menu.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Local> Locals { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // many-to-many: Category - Tag
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Tags)
                .WithMany(t => t.Categories);
        }
    }


}
