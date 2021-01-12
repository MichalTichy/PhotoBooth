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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<OrderProduct>().HasKey(item => new {item.OrderId, item.ItemId});
            builder.Entity<OrderProduct>().HasOne<Order>(item => item.Order).WithMany(order => order.OrderItems);

            builder.Entity<OrderRentalItem>().HasKey(item => new {item.OrderId, item.ItemId});
            builder.Entity<OrderRentalItem>().HasOne<Order>(item => item.Order).WithMany(order => order.RentalItems);

            builder.Entity<ItemPackageProduct>().HasKey(item => new { item.ItemPackageId, item.ProductId });
            builder.Entity<ItemPackageProduct>().HasOne<ItemPackage>(product => product.ItemPackage).WithMany(package => package.Products);

            builder.Entity<ItemPackageRentalItem>().HasKey(item => new { item.ItemPackageId, item.RentalItemType });
            builder.Entity<ItemPackageRentalItem>().HasOne<ItemPackage>(product => product.ItemPackage).WithMany(package => package.RentalItems);
            base.OnModelCreating(builder);
        }
    }

}
