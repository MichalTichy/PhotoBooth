using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Queries;
using PhotoBooth.DAL.Entity;
using System;
using System.Collections.Generic;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Repository;
using Riganti.Utils.Infrastructure.Core;

namespace PhotoBooth.BL.Facades
{
    public class CatalogFacade : FacadeBase<ProductModel>, ICatalogFacade
    {
        public CatalogFacade(BaseRepository<ProductModel> repository, IUnitOfWorkProvider uow) : base(repository, uow)
        {
        }

        public bool AreAllRentalItemsAvailable(ICollection<RentalItemModel> items, DateTime since, DateTime till)
        {
            throw new NotImplementedException();
        }

        public ICollection<ItemPackage> GetAllPackages()
        {
            throw new NotImplementedException();
        }

        public ICollection<ProductModel> GetAvailableProducts(DateTime since, DateTime till)
        {
            var query = new ProductsQuery(base.UnitOfWorkFactory);
            query.AddSortCriteria(x => x.AmountLeft > 0);
            return query.Execute();
        }

        public ICollection<RentalItemModel> GetAvailableRentalItems(DateTime since, DateTime till, RentalItemType? type = null)
        {
            throw new NotImplementedException();
        }
    }
}
