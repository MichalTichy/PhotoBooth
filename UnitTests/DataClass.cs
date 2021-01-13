using PhotoBooth.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    public static class DataClass
    {
        private static string[] names = {
            "Niesha Knipp ",
            "Maia Ortmann ",
            "Fredricka Vantassell ",
            "Natalia Barlowe ",
            "Melony Mccool ",
            "Lakeshia Milling ",
            "Emmitt Decosta ",
            "Dolly Walston ",
            "Britni Motto ",
            "Nga Bustos ",
            "Randall Helvey ",
            "Jazmin Vicente ",
            "Katherine Laclair ",
            "Kimberely Rexrode ",
            "Maple Rainey ",
            "Karole Segraves ",
            "Gena Prieto ",
            "Johnetta Shirah ",
            "Tamekia Martine ",
            "Albert Zachary ",
            "Katie Helmuth ",
            "Delaine Colas ",
            "Rosaura Arakaki ",
            "Jefferey Pomerleau ",
            "Olen Balog ",
            "Angelena Freel ",
            "Julieta Mirarchi ",
            "Federico Killingsworth ",
            "Alonzo Hubbert ",
            "Jenna Lemon ",
            "Barney Grote ",
            "Eboni Malm ",
            "Delmy Westra ",
            "Jermaine Money ",
            "Tiffani Bodily ",
            "Adelaide Gowan ",
            "Bea Hentz ",
            "Jamey Cartier ",
            "Clarence Lozano ",
            "Meryl Talamantez ",
            "Candis Ashby ",
            "Yetta Violette ",
            "Velia Teran ",
            "Dalila Gravitt ",
            "Coralee Domenico ",
            "Ethel Kuehl ",
            "Astrid Carrasco ",
            "Mason Boyers ",
            "Melina Hoadley ",
            "Gita Dejoh"
        };
        public static List<Address> Addresses = new List<Address>();
        public static List<Order> Orders = new List<Order>();
        public static List<ItemPackage> ItemPackages = new List<ItemPackage>();
        public static List<ApplicationUser> ApplicationUsers = new List<ApplicationUser>();
        public static List<RentalItem> RentalItems = new List<RentalItem>();
        public static List<Product> Products = new List<Product>();
        public static DateTime since = new DateTime(1998, 9, 11);
        public static DateTime till = new DateTime(1999, 9, 11);
        //data by v sebe mali mat aspon 5 hodnot
        static DataClass()
        {

            for (int i = 0; i < 25; i++)
            {
                DataClass.Addresses.Add(new Address() { Id = Guid.NewGuid(), City = "Kosice", PostalCode = i.ToString(), Street = "lunik " + i, BuildingNumber = "azp" + (i * i) });
            }
            for (int i = 0; i < 100; i++)
            {
                DataClass.RentalItems.Add(new RentalItem() { Id = Guid.NewGuid(), Name = "Pozadie0" + i, PictureUrl = "field" + i });
            }
            for (int i = 0; i < 25; i++)
            {
                DataClass.Products.Add(new Product() { Id = Guid.NewGuid(), Name = "fotka0" + i, PictureUrl = "new Photo of" + i });
            }
            for (int i = 0; i < 25; i++)
            {
                DataClass.ItemPackages.Add(
                    new ItemPackage()
                    {
                        Id = Guid.NewGuid(),
                        Name = "Package" + i,
                        CurrentlyAvailable = i % 2 == 0,
                        RentalItems = RentalItems.Take(12).Select(x => new ItemPackageRentalItem() { ItemPackageId = x.Id, RentalItemType = x.Type, }).ToList()

                    });
            }
            for (int i = 0; i < 6; i++)
            {
                Orders.Add(
                new Order()
                {
                    Id = Guid.NewGuid(),
                    CustomerId = i.ToString(),
                    RentalItems = DataClass.RentalItems.Take(13).Select(x => new OrderRentalItem() { ItemId = x.Id, Item = x }).ToList(),
                    RentalSince = since,
                    RentalTill = till,
                    BannerUrl = Guid.NewGuid().ToString()
                });
            }
        }
    }
}
