using NUnit.Framework;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System;
using System.Linq;

namespace UnitTests
{
    class RentalItemRepositaryTest : RepositoryBaseTest<RentalItem>
    {
        public RentalItemRepositaryTest() : base(DataClass.RentalItems) { }

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
                product.Name = data;
                DataRepo.Update(product);
                uow.Commit();

                var productFromDb = GetRepo(uow).Find(product.Id);
                Assert.NotNull(productFromDb);
                Assert.AreEqual(data, productFromDb.Name);
                DataRepo.Delete(DataList.First().Id);
                uow.Commit();
            }
        }
    }
}
