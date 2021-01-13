using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
using NUnit.Framework;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Repository;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PhotoBooth.DAL.Entity;

namespace UnitTests
{
    public abstract class RepositoryBaseTest<TEntity> where TEntity : class, IEntity<Guid>, new()
    {
        protected static List<TEntity> DataList;

        protected EntityFrameworkUnitOfWorkProvider<PhotoBoothContext> UnitOfWorkProvider;
        protected BaseRepository<TEntity> DataRepo;
        public RepositoryBaseTest(List<TEntity> data)
        {
            DataList = data;
        }
        [SetUp]
        public void SetupDatabase()
        {
            UnitOfWorkProvider = new TestUnitOfWorkProvider(Guid.NewGuid().ToString());
            DataRepo = new BaseRepository<TEntity>(UnitOfWorkProvider, new LocalDateTimeProvider());
        }
        [Test, Order(1)]
        public void TestInsertAndDelete()
        {
            var temp = DataList[0];
            var id = temp.Id;
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                DataRepo.Insert(temp);
                uow.Commit();
            }

            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var data = GetRepo(uow).ToList();
                Assert.AreEqual(1, data.Count);
                //Assert.AreEqual(data.Single().Id, id, DataList[0].ToString());
            }

            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {

                var data = GetRepo(uow).ToList();
                Assert.AreEqual(1, data.Count, "Insert test does not work thus this test can not be tested");

                DataRepo.Delete(data.Single().Id);
                uow.Commit();

                var productsFromDb = GetRepo(uow).ToList();
                CollectionAssert.IsEmpty(productsFromDb);
            }
        }

        [Test, Order(2)]
        public void TestInsertRangeDeleteRange()
        {
            var size = 5;
            var temp = DataList.Take(size).ToList();
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                DataRepo.Insert(temp);
                uow.Commit();
            }

            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var data = GetRepo(uow).ToList();
                Assert.AreEqual(size, data.Count);
                //Assert.AreEqual(data, temp, "fdsafsdfasdf "+ data.Select(x => x.Id.ToString()).ToString() + temp.Select(x => x.Id.ToString()));
            }

            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {

                DataRepo.Delete(temp.Select(x => x.Id));
                uow.Commit();

                var productsFromDb = GetRepo(uow).ToList();
                CollectionAssert.IsEmpty(productsFromDb);
            }
        }

        [Test, Order(3)]
        public async System.Threading.Tasks.Task TestInsertRangeGetRangeAsync()
        {
            var size = 5;
            var temp = DataList.Take(size);
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                DataRepo.Insert(temp);
                uow.Commit();
            }

            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var data = GetRepo(uow).ToList();
                Assert.AreEqual(size, data.Count);
                //Assert.AreEqual(data, temp);
            }

            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {

                var tData = await DataRepo.GetByIdsAsync(temp.Select(x => x.Id));
                uow.Commit();

                var productsFromDb = GetRepo(uow).ToList();
                Assert.AreEqual(size, tData.Count);
                //Assert.AreEqual(tData, temp); ;
            }
        }

        /*[Test]
        public void BasicTestUpdate()
        {
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var context = GetBoothContext(uow);

                var product = DataList.First();
                GetRepo(uow).Add(product);
                uow.Commit();

                var id = Guid.NewGuid();
                product.Id = id;
                DataRepo.Update(product);
                uow.Commit();

                var productFromDb = context.Products.Find(product.Id);
                Assert.NotNull(productFromDb);
                Assert.AreEqual(id, productFromDb.Id);
                DataRepo.Delete(id);
                uow.Commit();
            }
        }*/
        [Test]
        public virtual void VirtualTestUpdate()
        {
            throw new Exception("not implemented");
        }

        [Test]
        public void TestGet()
        {
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var context = GetBoothContext(uow);

                var product = DataList.First();
                GetRepo(uow).Add(product);
                uow.Commit();

                var productFromDb = DataRepo.GetById(product.Id);

                Assert.IsNotNull(productFromDb);
                Assert.AreEqual(product, productFromDb);
                DataRepo.Delete(product.Id);
            }
        }

        public DbSet<TEntity> GetRepo(EntityFrameworkUnitOfWork<PhotoBoothContext> uow)
        {
            object temp = null;
            if (typeof(TEntity) == typeof(Address))
                temp = GetBoothContext(uow).Addresses;
            if (typeof(TEntity) == typeof(ItemPackage))
                temp = GetBoothContext(uow).ItemPackages;
            if (typeof(TEntity) == typeof(Order))
                temp = GetBoothContext(uow).Orders;
            if (typeof(TEntity) == typeof(Product))
                temp = GetBoothContext(uow).Products;
            if (typeof(TEntity) == typeof(RentalItem))
                temp = GetBoothContext(uow).RentalItems;
            if (typeof(TEntity) == typeof(ApplicationUser))
                temp = GetBoothContext(uow).Users;


            return (DbSet<TEntity>)temp;
        }
        protected PhotoBoothContext GetBoothContext(EntityFrameworkUnitOfWork<PhotoBoothContext> unitOfWork)
        {
            return unitOfWork.Context;
        }
    }
}
