using NUnit.Framework;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.BL.Queries;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWorkModels;
using PhotoBooth.DAL.UnitOfWorkProviderModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{

    public class QueryTest
    {
        public string databaseStr = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=IlinovTestDB011";
        public List<Address> Addresses = new List<Address>();
        public List<ApplicationUser> Users = new List<ApplicationUser>();
        public List<Order> Orders = new List<Order>();
        public List<ItemPackage> ItemPackages = new List<ItemPackage>();
        public List<ApplicationUser> ApplicationUsers = new List<ApplicationUser>();
        public List<RentalItem> RentalItems = new List<RentalItem>();
        public DateTime since = new DateTime(1998, 9, 11);
        public DateTime till = new DateTime(1999, 9, 11);
        [SetUp]
        public void SetUp()
        {
            for (int i = 0; i < 25; i++)
            {
                Addresses.Add(new Address() { City = "Kosice", PostalCode = i.ToString(), Street = "lunik " + i, BuildingNumber = "azp" + (i * i) });
            }
            for (int i = 0; i < 25; i++)
            {
                ApplicationUsers.Add(new ApplicationUser("hektor" + i) { FirstName = "Macko", LastName = "Usko" + i });
            }
            for (int i = 0; i < 100; i++)
            {
                RentalItems.Add(new RentalItem { Id = Guid.NewGuid(), Name = "fotka0" + i, PictureUrl = "new Photo of" + i });
            }
            Orders.Add(new Order() { Id = Guid.NewGuid(), RentalItems = RentalItems.Take(13).ToList(), RentalSince = since, RentalTill = till });

            using (var db = new PhotoBoothContext(databaseStr))
            {
                db.Database.EnsureCreated();
                db.SaveChanges();
            }
            
            using (var uow = new UnitOfWork(databaseStr))
            {
                Addresses.ForEach(x => uow.AddressRepository.Create(x));
                ApplicationUsers.ForEach(x => uow.ApplicationUserRepository.Create(x));
                RentalItems.ForEach(x => uow.RentalItemRepository.Create(x));
                Orders.ForEach(x => uow.OrderRepository.Create(x));
                uow.Save();
            }
        }

        [Test]
        public void TestIfDataEmpty()
        {
            /* this code needs to be in first test as it creates database and fills it, and if it is in setup it does throws errors,
             * this cemented part of code mus be run only once
             * 
            
            */



            using (var uow = new UnitOfWork(databaseStr))
            {
                var temp = uow.AddressRepository.Get();
                var t = temp.Any();
                Assert.IsTrue(t, "database is empty");
                /*
                foreach (var address in uow.AddressRepository.Get())
                {
                    s = s + address.City + " " + address.PostalCode + " street: " + address.Street + " buildingNum: " + address.BuildingNumber + "\n";
                }*/
            }
            
        }
        [Test]
        public void TestAddressQuery()
        {

            AddressQuery temp = new AddressQuery(new UnitOfWorkProvider(databaseStr));
            var query = temp.ExecuteAsync().ToList();
            var listA = Addresses.Take(10).Select(x => new AddressModel()
            { BuildingNumber = x.BuildingNumber, City = x.City, Id = x.Id, PostalCode = x.PostalCode, Street = x.Street })
                .ToList();
            Assert.AreSame(listA.Select(x => x.ToString()), query.Select(x => x.ToString()),
                "addressquery does not work correctly" +
                query.Aggregate("", (a, address) => a + " \n" + address)
                + "\n\n\n --------------------------"
                + listA.Aggregate("", (a, address) => a + " \n" + address));

        }



        [Test]
        public void TestAvailableQuery()
        {
            using (var uow = new UnitOfWork(databaseStr))
            {
                throw new Exception(uow.OrderRepository.Get().ToList().Aggregate("", (a,b)=> a + " \n " + b.RentalItems.Count().ToString()));
            }
                var temp = new AvailableRentalItems(since, till, new UnitOfWorkProvider(databaseStr));
            Assert.AreEqual(temp.ExecuteAsync().Count, 90);
        }

        [TearDown]
        public void TearDownMethod()
        {
            using (var db = new PhotoBoothContext(databaseStr))
            {
                db.Database.EnsureDeleted();
                db.SaveChanges();
            }
        }
    }
}
