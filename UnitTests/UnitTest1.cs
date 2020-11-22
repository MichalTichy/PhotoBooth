using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PhotoBooth.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using PhotoBooth.DAL.Repository;
using PhotoBooth.DAL.Entity;
using DbContextOptions = Riganti.Utils.Infrastructure.EntityFrameworkCore.DbContextOptions;

namespace UnitTests
{
    public class Tests
    {

        
        [SetUp]
        public void Setup()
        {
        }

        public class TestUnitOfWorkProvider : EntityFrameworkUnitOfWorkProvider<PhotoBoothContext>
        {


            public TestUnitOfWorkProvider() : base(new AsyncLocalUnitOfWorkRegistry(), DbContextFactory)
            {
            }

            private static PhotoBoothContext DbContextFactory()
            {
                var optionsBuilder = new DbContextOptionsBuilder<PhotoBoothContext>();


                optionsBuilder.UseInMemoryDatabase( Guid.NewGuid().ToString());
                optionsBuilder.EnableSensitiveDataLogging();

                return new PhotoBoothContext(optionsBuilder.Options);
            }
        }
        public class DateTimeProvider : IDateTimeProvider
        {
            public DateTime Now => DateTime.Now;
        }


        [Test]
        public async System.Threading.Tasks.Task Test1Async()
        {
            var uowProvider = new TestUnitOfWorkProvider();
            var idSpeci = Guid.NewGuid();
            var repo = new BaseRepository<Product>(uowProvider, new DateTimeProvider());
            using (var uow = uowProvider.Create())
            {
                var t = repo.GetById(idSpeci);
                Assert.AreEqual(t, null);
            }
            //insert getById
            using (var uow = uowProvider.Create())
            {
                repo.Insert(new Product() { Id = idSpeci, Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", });
                var t = repo.GetById(idSpeci);
                Assert.IsTrue(t != null);
            }
            //insert and store
            using (var uow = uowProvider.Create())
            {
                var t = repo.GetById(idSpeci);
                Assert.IsTrue(t == null);
                repo.Insert(new Product() { Id = idSpeci, Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", });
                uow.Commit();
            }

            using (var uow = uowProvider.Create())
            {
                var t = repo.GetById(idSpeci);
                Assert.IsTrue(t != null);
            }
            //delete 
            using (var uow = uowProvider.Create())
            {
                repo.Delete(idSpeci);
                var t = repo.GetById(idSpeci);
                Assert.IsTrue(t == null);
                uow.Commit();
            }
            using (var uow = uowProvider.Create())
            {
                repo.Delete(idSpeci);
                var t = repo.GetById(idSpeci);
                Assert.IsTrue(t == null);
                uow.Commit();
            }
            using (var uow = uowProvider.Create())
            {
                var t = repo.GetById(idSpeci);
                Assert.IsTrue(t == null);
            }
        }
    }
}