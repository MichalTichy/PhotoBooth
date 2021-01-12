using System;
using System.Collections.Generic;
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

            #region seeding
            //seeding rentalItems
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Retro fotobudka", DescriptionHtml = "Unikátna, skvele vyzerajúca fotobúdka s neobmedzenou možnosťou tlače v krásnom retro dizajne.", PricePerHour = 80, Type = RentalItemType.PhotoBooth, PictureUrl = "https://photos.smileshoot.sk/photobooth.jpg" });
            //employees
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Dominik", DescriptionHtml = "Najlepsi zamestnanec roka", PricePerHour = 10, Type = RentalItemType.Employe });
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Milos", DescriptionHtml = "2. najlepsi zamestnanec roka", PricePerHour = 10, Type = RentalItemType.Employe });
            //backgrounds
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Pozadie A", DescriptionHtml = "Zlate pozadie s gulickami", PricePerHour = 10, Type = RentalItemType.Background, PictureUrl = "https://photos.smileshoot.sk/pozadie-A.jpg" });
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Pozadie B", DescriptionHtml = "Kvetinove pozadie", PricePerHour = 10, Type = RentalItemType.Background, PictureUrl = "https://photos.smileshoot.sk/pozadie-B.jpg" });
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Pozadie C", DescriptionHtml = "Vintage pozadie", PricePerHour = 10, Type = RentalItemType.Background, PictureUrl = "https://photos.smileshoot.sk/pozadie-C.jpg" });
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Pozadie D", DescriptionHtml = "Vianocne pozadie s vlockami", PricePerHour = 10, Type = RentalItemType.Background, PictureUrl = "https://photos.smileshoot.sk/pozadie-D.jpg" });
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Pozadie E", DescriptionHtml = "Svieze modre pozadie", PricePerHour = 10, Type = RentalItemType.Background, PictureUrl = "https://photos.smileshoot.sk/pozadie-E.jpg" });
            //props
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Vianocne rekvizity", DescriptionHtml = "Santa claus ciapky, vianocne okuliare...", PricePerHour = 10, Type = RentalItemType.Prop, PictureUrl = "https://photos.smileshoot.sk/props1.jpg" });
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Svadobne rekvizity", DescriptionHtml = "Tabulky 'nabuduce sa vydavam ja', 'parketovy lev', parochne, klobuky...", PricePerHour = 10, Type = RentalItemType.Prop, PictureUrl = "https://photos.smileshoot.sk/props2.jpg" });
            builder.Entity<RentalItem>().HasData(new RentalItem { Id = Guid.NewGuid(), Name = "Party mix rekvizity", DescriptionHtml = "Smiesne parochne, okuliare...", PricePerHour = 10, Type = RentalItemType.Prop, PictureUrl = "https://photos.smileshoot.sk/props3.jpg" });

            //seeding products
            var usb = new Product { Id = Guid.NewGuid(), Name = "USB kluc", AmountLeft = 20, Price = 5, DescriptionHtml = "Usb so zhotovenymi fotkami", PictureUrl = "https://photos.smileshoot.sk/usb.jpg" };
            var book = new Product { Id = Guid.NewGuid(), Name = "Fotokniha", AmountLeft = 10, Price = 10, DescriptionHtml = "Fotokniha s drevenou prednou stranou + gravirovanie", PictureUrl = "https://photos.smileshoot.sk/fotokiha.jpg" };
            builder.Entity<Product>().HasData(usb);
            builder.Entity<Product>().HasData(book);

            //seeding packages
            //packageL
            var packageL = new ItemPackage { Id = Guid.NewGuid(), CurrentlyAvailable = true, Name = "Balik L" };
            builder.Entity<ItemPackageProduct>().HasData(new ItemPackageProduct { ItemPackageId = packageL.Id, ProductId = usb.Id });
            builder.Entity<ItemPackageProduct>().HasData(new ItemPackageProduct { ItemPackageId = packageL.Id, ProductId = book.Id });
            builder.Entity<ItemPackageRentalItem>().HasData(new ItemPackageRentalItem { RentalItemType = RentalItemType.Background, ItemPackageId = packageL.Id });
            builder.Entity<ItemPackageRentalItem>().HasData(new ItemPackageRentalItem { RentalItemType = RentalItemType.PhotoBooth, ItemPackageId = packageL.Id });
            builder.Entity<ItemPackageRentalItem>().HasData(new ItemPackageRentalItem { RentalItemType = RentalItemType.Employe, ItemPackageId = packageL.Id });
            builder.Entity<ItemPackageRentalItem>().HasData(new ItemPackageRentalItem { RentalItemType = RentalItemType.Prop, ItemPackageId = packageL.Id });
            builder.Entity<ItemPackage>().HasData(packageL);
            //packageM
            var packageM = new ItemPackage { Id = Guid.NewGuid(), CurrentlyAvailable = true, Name = "Balik M" };
            builder.Entity<ItemPackageRentalItem>().HasData(new ItemPackageRentalItem { RentalItemType = RentalItemType.PhotoBooth, ItemPackageId = packageM.Id });
            builder.Entity<ItemPackageRentalItem>().HasData(new ItemPackageRentalItem { RentalItemType = RentalItemType.Employe, ItemPackageId = packageM.Id });
            builder.Entity<ItemPackage>().HasData(packageM);

            #endregion
            base.OnModelCreating(builder);
        }
    }

}
