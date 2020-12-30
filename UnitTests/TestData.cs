using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWorkProviderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{

    public static class TestData
    {
        public static string databaseStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IlinovTestDB011";
        public static UnitOfWorkProvider provider = new UnitOfWorkProvider(databaseStr);
        public static List<Address> Addresses = new List<Address>();
        public static List<ApplicationUser> Users = new List<ApplicationUser>();
        public static List<Order> Orders = new List<Order>();
        public static List<ItemPackage> ItemPackages = new List<ItemPackage>();
        public static List<ApplicationUser> ApplicationUsers = new List<ApplicationUser>();
        public static List<RentalItem> RentalItems = new List<RentalItem>();
        public static DateTime since = new DateTime(1998, 9, 11);
        public static DateTime till = new DateTime(1999, 9, 11);
        static TestData()
        {

            for (int i = 0; i < 25; i++)
            {
                TestData.Addresses.Add(new Address() { City = "Kosice", PostalCode = i.ToString(), Street = "lunik " + i, BuildingNumber = "azp" + (i * i) });
            }
            for (int i = 0; i < 25; i++)
            {
                TestData.ApplicationUsers.Add(new ApplicationUser("hektor" + i) { FirstName = "Macko", LastName = "Usko" + i });
            }
            for (int i = 0; i < 100; i++)
            {
                TestData.RentalItems.Add(new RentalItem { Id = Guid.NewGuid(), Name = "fotka0" + i, PictureUrl = "new Photo of" + i });
            }
            TestData.Orders.Add(new Order() { Id = Guid.NewGuid(), RentalItems = TestData.RentalItems.Take(13).ToList(), RentalSince = since, RentalTill = till });

            using (var db = new PhotoBoothContext(TestData.databaseStr))
            {
                db.Database.EnsureCreated();
                db.SaveChanges();
            }
        }
    }
}
