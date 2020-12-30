using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.DAL
{
    public class PhotoBoothContext : IdentityDbContext<ApplicationUser>
    {
        public override void Dispose()
        {
            base.Dispose();
        }

        public override ValueTask DisposeAsync()
        {
            return base.DisposeAsync();
        }

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
