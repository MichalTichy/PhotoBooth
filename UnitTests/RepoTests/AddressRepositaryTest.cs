using NUnit.Framework;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests
{
    class AddressRepositaryTest : RepositoryBaseTest<Address>
    {
        public AddressRepositaryTest() : base(DataClass.Addresses) { }
        
        [Test]
        public override void VirtualTestUpdate()
        {
            using (var uow = (EntityFrameworkUnitOfWork<PhotoBoothContext>)UnitOfWorkProvider.Create())
            {
                var context = GetBoothContext(uow);

                var product = DataList.First();
                GetRepo(uow).Add(product);
                uow.Commit();

                var data = "postalCodeMine";
                product.PostalCode = data;
                DataRepo.Update(product);
                uow.Commit();

                var productFromDb = GetRepo(uow).Find(product.Id);
                Assert.NotNull(productFromDb);
                Assert.AreEqual(data, productFromDb.PostalCode);
                DataRepo.Delete(DataList.First().Id);
                uow.Commit();
            }
        }
    }
}
