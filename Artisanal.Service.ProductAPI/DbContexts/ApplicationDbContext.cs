using Artisanal.Service.ProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace Artisanal.Service.ProductAPI.DbContexts
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options  ):base(options)
        {
                

        }
        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasData(new Product

            {
                ProductId = 1,
                ProductName = "ChaiseBoisMassif",
                Price=15,
                CategoryName="categorie1",
                ImageURL="1.jpg"

            }) ;
            modelBuilder.Entity<Product>().HasData(new Product

            {
                ProductId = 2,
                ProductName = "ChaiseBoisMassif",
                Price = 15,
                CategoryName = "categorie1",
                ImageURL = "1.jpg"

            });
        }

    }
}
