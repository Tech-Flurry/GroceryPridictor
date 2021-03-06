using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryPridictor.Model
{
    public class GroceryContext : DbContext
    {

        public GroceryContext(DbContextOptions<GroceryContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<Store> Store { get; set; }
        public DbSet<StoreCategory> StoreCategory { get; set; }
        public DbSet<productModel> productModel { get; set; }
        public DbSet<storenewModel> storenewModels { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Product>().ToTable("Product");
            modelBuilder.Entity<Store>().ToTable("Store");
            modelBuilder.Entity<StoreCategory>().ToTable("StoreCategory");
            modelBuilder.Entity<productModel>().ToTable("productModel");
        }
    }
}
