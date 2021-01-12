using PhotoBooth.BL.Models;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Queries;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.Repository;
using Riganti.Utils.Infrastructure.Core;
using System;
using System.Collections.Generic;
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
            using (UnitOfWorkFactory.Create())
            {
                var available = new AvailableRentalItems(UnitOfWorkFactory, since, till).Execute();
                return items.All(x => available.Contains(x));
            }
        }


        public ICollection<ItemPackageDTO> GetAllPackages()
        {
            using (UnitOfWorkFactory.Create())
            {
                return new ItemPackagesQuery(UnitOfWorkFactory).Execute();
            }
        }

        public ICollection<ProductModel> GetAvailableProducts()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                
                var query = new AvailableProductsQuery(UnitOfWorkFactory);
                return query.Execute();
            }
        }

        public ICollection<RentalItemModel> GetAvailableRentalItems(DateTime since, DateTime till, RentalItemType? type = null)
        {
            using (UnitOfWorkFactory.Create())
            {
                var query = new AvailableRentalItems(UnitOfWorkFactory, since, till, type);
                return query.Execute();
            }
        }
    }
}
