using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.DAL
{
    public class PhotoBoothContext : IdentityDbContext<ApplicationUser>
    {
        public PhotoBoothContext()
        {

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


    }

}
