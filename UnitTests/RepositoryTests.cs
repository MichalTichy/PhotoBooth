using NUnit.Framework;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System;
using System.Linq;

namespace UnitTests
{
    public class RepositoryTests
    {
        private EntityFrameworkUnitOfWorkProvider<PhotoBoothContext> UnitOfWorkProvider;
        private BaseRepository<Product> ProductsRepository;

        [SetUp]
        public void SetupDatabase()
        {
            UnitOfWorkProvider = new TestUnitOfWorkProvider();
            ProductsRepository = new BaseRepository<Product>(UnitOfWorkProvider, new LocalDateTimeProvider());
        }
        [Test]
        public void TestInsert()
        {
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var id = Guid.NewGuid();
                ProductsRepository.Insert(new Product() { Id = id, Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", });
                uow.Commit();

                var products = GetBoothContext(uow).Products.ToList();
                Assert.AreEqual(1, products.Count);
                Assert.AreEqual(products.Single().Id, id);
            }
        }
        [Test]
        public void TestUpdate()
        {
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var context = GetBoothContext(uow);
                
                var product = new Product() { Id = Guid.NewGuid(), Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", };
                context.Products.Add(product);
                uow.Commit();

                product.Name = "name2";
                ProductsRepository.Update(product);
                uow.Commit();

                var productFromDb = context.Products.Find(product.Id);
                Assert.NotNull(productFromDb);
                Assert.AreEqual("name2",productFromDb.Name);
            }
        }
        [Test]
        public void TestDelete()
        {
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var context = GetBoothContext(uow);
                
                var product = new Product() { Id = Guid.NewGuid(), Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", };
                context.Products.Add(product);
                uow.Commit();

                product.Name = "name2";
                ProductsRepository.Delete(product.Id);
                uow.Commit();

                var productsFromDb = context.Products.ToList();
                CollectionAssert.IsEmpty(productsFromDb);
            }
        }
        [Test]
        public void TestGet()
        {
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var context = GetBoothContext(uow);
                
                var product = new Product() { Id = Guid.NewGuid(), Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", };
                context.Products.Add(product);
                uow.Commit();

                var productFromDb = ProductsRepository.GetById(product.Id);

                Assert.IsNotNull(productFromDb);
                Assert.AreEqual(product,productFromDb);
            }
        }

        protected PhotoBoothContext GetBoothContext(EntityFrameworkUnitOfWork<PhotoBoothContext> unitOfWork)
        {
            return unitOfWork.Context;
        }
    }
}