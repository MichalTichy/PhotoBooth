using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Queries;
using PhotoBooth.DAL.Entity;
using System;
using System.Collections.Generic;
using Riganti.Utils.Infrastructure.Core;


namespace PhotoBooth.BL.Facades
{
    public class CatalogFacade : ICatalogFacade
    {
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
            return new ProductsQuery(new UnitOfWorkProviderBase());
        }

        public ICollection<RentalItemModel> GetAvailableRentalItems(DateTime since, DateTime till, RentalItemType? type = null)
        {
            throw new NotImplementedException();
        }
    }
}
