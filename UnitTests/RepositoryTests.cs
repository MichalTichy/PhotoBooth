using NUnit.Framework;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System;
using System.Linq;

namespace UnitTests
{
    public class RepositoryTests
    {
        [SetUp]
        public void Setup()
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
        }

        [Test, Order(1)]
        public void TestDelete()
        {
            using (var uow = TestData.provider.GetUinOfWork())
            {

                var product = new Product() { Id = Guid.NewGuid(), Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", };
                uow.GetRepo<Product>().Create(product);
                uow.Save();

                product.Name = "name2";
                uow.GetRepo<Product>().Delete(product.Id);
                uow.Save();

                var productsFromDb = uow.GetRepo<Product>().Get().ToList();
                CollectionAssert.IsEmpty(productsFromDb);
            }
        }
        [Test]
        public void TestUpdate()
        {
            using (var uow = TestData.provider.GetUinOfWork())
            {

                var product = new Product() { Id = Guid.NewGuid(), Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", };
                uow.GetRepo<Product>().Create(product);
                uow.Save();

                product.Name = "name2";
                uow.GetRepo<Product>().Update(product);
                uow.Save();

                var productFromDb = uow.GetRepo<Product>().Get(product.Id);
                Assert.NotNull(productFromDb);
                Assert.AreEqual("name2", productFromDb.Name);
            }
        }


        [Test]
        public void TestGet()
        {
            using (var uow = TestData.provider.GetUinOfWork())
            {
                var id = Guid.NewGuid();
                var product = new Product() { Id = id, Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", };
                uow.GetRepo<Product>().Create(product);
                uow.Save();

                var productFromDb = uow.GetRepo<Product>().Get(id);

                Assert.IsNotNull(productFromDb);
                Assert.AreEqual(product, productFromDb);
            }
        }

        [Test, Order(2)]
        public void TestInsert()
        {
            using (var uow = TestData.provider.GetUinOfWork())
            {
                var id = Guid.NewGuid();
                uow.GetRepo<Product>().Create(new Product() { Id = id, Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", });
                uow.Save();

                var products = uow.GetRepo<Product>().Get().ToList();
                Assert.AreEqual(1, products.Count);
                Assert.AreEqual(products.Single().Id, id);
            }
        }
    }

    public class RepositoryOrderTests
    {
        [SetUp]
        public void Setup()
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
        }

        [Test, Order(1)]
        public void TestInsertCollection()
        {
            Order order;
            using (var uow = TestData.provider.GetUinOfWork())
            {
                var id = Guid.NewGuid();
                uow.GetRepo<Order>().Create(new Order() { Id = id, RentalItems = TestData.RentalItems.Take(10).ToList() });
                uow.Save();
                order = uow.GetRepo<Order>().Get(id);
            }
            
            Assert.NotNull(order);
            Assert.NotNull(order.RentalItems);
            Assert.AreEqual(10, order.RentalItems.Count());
        }

        [Test, Order(2)]
        public void TestDelete()
        {
            using (var uow = TestData.provider.GetUinOfWork())
            {

                var product = new Product() { Id = Guid.NewGuid(), Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", };
                uow.GetRepo<Product>().Create(product);
                uow.Save();

                product.Name = "name2";
                uow.GetRepo<Product>().Delete(product.Id);
                uow.Save();

                var productsFromDb = uow.GetRepo<Product>().Get().ToList();
                CollectionAssert.IsEmpty(productsFromDb);
            }
        }
        [Test]
        public void TestUpdate()
        {
            using (var uow = TestData.provider.GetUinOfWork())
            {

                var product = new Product() { Id = Guid.NewGuid(), Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", };
                uow.GetRepo<Product>().Create(product);
                uow.Save();

                product.Name = "name2";
                uow.GetRepo<Product>().Update(product);
                uow.Save();

                var productFromDb = uow.GetRepo<Product>().Get(product.Id);
                Assert.NotNull(productFromDb);
                Assert.AreEqual("name2", productFromDb.Name);
            }
        }


        [Test]
        public void TestGet()
        {
            using (var uow = TestData.provider.GetUinOfWork())
            {
                var id = Guid.NewGuid();
                var product = new Product() { Id = id, Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", };
                uow.GetRepo<Product>().Create(product);
                uow.Save();

                var productFromDb = uow.GetRepo<Product>().Get(id);

                Assert.IsNotNull(productFromDb);
                Assert.AreEqual(product, productFromDb);
            }
        }

        
    }
}