using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PhotoBooth.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;
using System;
using PhotoBooth.DAL.Repository;
using PhotoBooth.DAL.Entity;

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
            public TestUnitOfWorkProvider() : base(new ThreadLocalUnitOfWorkRegistry(), DbContextFactory)
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
        public void Test1()
        {
            var uowProvider = new TestUnitOfWorkProvider();
            var idSpeci = Guid.NewGuid();
            var repo = new BaseRepository<Product>(uowProvider, new DateTimeProvider());
            using (var uow = uowProvider.Create())
            {
                //var dfdfd = EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider).ContextId;
                throw new Exception((EntityFrameworkUnitOfWork.TryGetDbContext(uowProvider)  == null )+ " this is the value inserted");

                repo.GetById(idSpeci);
                //repo.Insert(new Product() { Id = idSpeci, Name = "name", PictureUrl = "fffffff", DescriptionHtml = "fasdjfklajsdf", });
                throw new Exception(repo.GetById(idSpeci) + " this is the value inserted");
            }
        }
    }
}