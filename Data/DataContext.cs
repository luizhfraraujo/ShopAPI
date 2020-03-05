using Microsoft.EntityFrameworkCore;
using Shop.Models;

namespace Shop.Data {
    public class DataContext : DbContext {
        public DataContext (DbContextOptions<DataContext> options) : base (options) {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            modelBuilder.Entity<User> ().HasData (
                new User {
                    Id = 1,
                        Email = "admin@shop.com",
                        FirstName = "Admin",
                        LastName = "Shop",
                        Password = "password",
                        Role = "admin"
                }
            );
        }

    }
}