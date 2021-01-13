using NUnit.Framework;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System;
using System.Linq;

namespace UnitTests
{
    class OrderRepositaryTest : RepositoryBaseTest<Order>
    {
        public OrderRepositaryTest() : base(DataClass.Orders) { }

        [Test]
        public override void VirtualTestUpdate()
        {
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var context = GetBoothContext(uow);

                var product = DataList.First();
                GetRepo(uow).Add(product);
                uow.Commit();

                var data = Guid.NewGuid().ToString();
                product.BannerUrl = data;
                DataRepo.Update(product);
                uow.Commit();

                var productFromDb = GetRepo(uow).Find(product.Id);
                Assert.NotNull(productFromDb);
                Assert.AreEqual(data, productFromDb.BannerUrl);
                DataRepo.Delete(DataList.First().Id);
                uow.Commit();
            }
        }
    }
}
