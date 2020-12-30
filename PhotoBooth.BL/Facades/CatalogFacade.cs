using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Queries;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWorkProviderModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhotoBooth.BL.Facades
{
    public class CatalogFacade : FacadeBase<Product>, ICatalogFacade
    {
        public CatalogFacade(UnitOfWorkProvider provider) : base(provider)
        {
        }

        public bool AreAllRentalItemsAvailable(ICollection<RentalItemModel> items, DateTime since, DateTime till)
        {
            var availables = new AvailableRentalItems(since, till, provider).ExecuteAsync();
            return items.All(x => availables.Contains(x));
        }

        public ICollection<ItemPackageDTO> GetAllPackages()
        {
            return new ItemPackagesQuery(provider).ExecuteAsync();
        }

        public ICollection<ProductModel> GetAvailableProducts()
        {
            var query = new ProductsQuery(provider);
            query.Where(x => x.AmountLeft > 0);
            return query.ExecuteAsync();
        }

        public ICollection<RentalItemModel> GetAvailableRentalItems(DateTime since, DateTime till, RentalItemType? type = null)
        {
            return new AvailableRentalItems(since, till, provider).ExecuteAsync();
        }
    }
}
