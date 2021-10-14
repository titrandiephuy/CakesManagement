using System;
using CakesManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace CakesManagement.Contexts
{
    public class CakesManagementDBContext: DbContext
    {
        public CakesManagementDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Cake> Cakes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedingCategory(modelBuilder);
        }

        private void SeedingCategory(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                            new Category()
                            {
                                CategoryId = 1,
                                CategoryName = "Birthday Cake",
                                Description = "Cake for birthday"
                            }, new Category()
                            {
                                CategoryId = 2,
                                CategoryName = "Apple Cake",
                                Description = "Cake with apple"
                            }, new Category()
                            {
                                CategoryId = 3,
                                CategoryName = "Cheese Cake",
                                Description = "Cake with cheese"
                            }, new Category()
                            {
                                CategoryId = 4,
                                CategoryName = "French Cake",
                                Description = "Cake from France"
                            }, new Category()
                            {
                                CategoryId = 5,
                                CategoryName = "Vietnamese Cake",
                                Description = "Traditional Vietnam cake"
                            }
                );
        }
    }
}
