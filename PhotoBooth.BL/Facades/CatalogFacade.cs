using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Queries;
using PhotoBooth.DAL.Entity;
using System;
using System.Collections.Generic;
using PhotoBooth.DAL;
using PhotoBooth.DAL.Repository;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace PhotoBooth.BL.Facades
{
    public class CatalogFacade : FacadeBase<Product>, ICatalogFacade
    {
        public CatalogFacade(BaseRepository<Product> repository, IUnitOfWorkProvider uow) : base(repository, uow)
        {
        }

        public bool AreAllRentalItemsAvailable(ICollection<RentalItemModel> items, DateTime since, DateTime till)
        {
            var availables = new AvailableRentalItems(base.UnitOfWorkFactory, since, till).Execute();
            return items.All(x => availables.Contains(x));
        }

        public ICollection<ItemPackageDTO> GetAllPackages()
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
            return new AvailableRentalItems(base.UnitOfWorkFactory, since, till).Execute();
        }
    }
}
