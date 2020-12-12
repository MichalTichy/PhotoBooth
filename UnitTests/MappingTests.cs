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
    public class MappingTests
    {
        private EntityFrameworkUnitOfWorkProvider<PhotoBoothContext> UnitOfWorkProvider;
        private BaseRepository<Order> OrdersRepository;

        [SetUp]
        public void SetupDatabase()
        {
            UnitOfWorkProvider = new TestUnitOfWorkProvider();
            OrdersRepository = new BaseRepository<Order>(UnitOfWorkProvider, new LocalDateTimeProvider());
        }

        [Test]
        public void OrderListQueryTest()
        {
            Order o = new Order(){
                RentalSince = new DateTime(2020, 12, 3, 16, 0, 0),
                RentalTill = new DateTime(2020, 12, 3, 23, 0, 0),
                Created = new DateTime(2020, 10, 3, 16, 0, 0),
                FinalPrice = 1234.ToString()
            };
            using (var uow = UnitOfWorkProvider.Create())
            {
                OrdersRepository.Insert(o);
                uow.Commit();
            }
        }

        protected PhotoBoothContext GetBoothContext(EntityFrameworkUnitOfWork<PhotoBoothContext> unitOfWork)
        {
            return unitOfWork.Context;
        }
    }
}