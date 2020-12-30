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

        public static bool loadDbStart = true;
        public static bool destroyDbEnd = false;
        [SetUp]
        public void SetUp()
        {
            if (loadDbStart)
            {
                using (var db = new PhotoBoothContext(TestData.databaseStr))
                {
                    var t = db.Database.EnsureDeleted();
                    db.SaveChanges();
                }

                using (var db = new PhotoBoothContext(TestData.databaseStr))
                {
                    db.Database.EnsureCreated();
                    db.SaveChanges();
                }

                using (var uow = TestData.provider.GetUinOfWork())
                {


                    TestData.Addresses.ForEach(x => uow.GetRepo<Address>().Create(x));
                    TestData.ApplicationUsers.ForEach(x => uow.GetRepo<ApplicationUser>().Create(x));
                    //RentalItems.ForEach(x => uow.GetRepo<RentalItem>().Create(x));
                    uow.GetRepo<Order>().Create(new Order() { Id = Guid.NewGuid(), RentalItems = TestData.RentalItems.Take(10).ToList(), RentalSince = TestData.since, RentalTill = TestData.till });
                    uow.Save();
                }
            }

        }

        [Test]
        public void ThisShouldThrow()
        {
            using (var uow = TestData.provider.GetUinOfWork())
            {
                throw new Exception(
                    "ADDRESES: \n"+
                    uow.GetRepo<Address>().Get().Aggregate("", (a, b) => a + " " + b + "\n") +
                    "Users:\n"+
                    "Orders: \n"+
                    uow.GetRepo<Order>().Get().Aggregate("", (a, b) => a + " " + b.Id + " " + b?.RentalItems?.Aggregate("list :", (c,d)=> c + ", " +  d.Name) + "\n"));
            }
        }
        [Test]
        public void TestAddressQuery() // this works but doesnt
        {

            AddressQuery temp = new AddressQuery(new UnitOfWorkProvider(TestData.databaseStr));
            var query = temp.ExecuteAsync().ToList();
            var listA = TestData.Addresses.Take(10).Select(x => new AddressModel()
            { BuildingNumber = x.BuildingNumber, City = x.City, Id = x.Id, PostalCode = x.PostalCode, Street = x.Street })
                .ToList();
            Assert.AreSame(listA.Select(x => x.ToString()), query.Select(x => x.ToString()),
                "addressquery does not work correctly, query count " + query.Count() + "list count :" + listA.Count() +
                query.Aggregate("", (a, address) => a + " \n" + address)
                + "\n\n\n --------------------------"
                + listA.Aggregate("", (a, address) => a + " \n" + address)) ;

        }



        [Test]
        public void TestAvailableQuery()
        {
            using (var uow = TestData.provider.GetUinOfWork())
            {
                var t = uow.GetRepo<Order>().Get().ToList();
                var s = t.Aggregate("", (a, b) => a + " \n " + b.RentalItems?.Count().ToString());
                throw new Exception(s);
            }
                var temp = new AvailableRentalItems(TestData.since, TestData.till, TestData.provider);
            Assert.AreEqual(temp.ExecuteAsync().Count, 90);
        }

        [TearDown]
        public void TearDownMethod()
        {
            if (destroyDbEnd)
            {
                using (var db = new PhotoBoothContext(TestData.databaseStr))
                {
                    db.Database.EnsureDeleted();
                    db.SaveChanges();
                }
            }
            
        }
    }
}
