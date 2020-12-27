using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.DAL
{
    public class PhotoBoothContext : DbContext
    {
        private string connectionString { get; set; }
        public PhotoBoothContext() { }
        public PhotoBoothContext(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public PhotoBoothContext(DbContextOptions options)
            : base(options)
        {

        }

        public DbSet<RentalItem> RentalItems { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<ItemPackage> ItemPackages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }
    }

}
