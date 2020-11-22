using System;
using System.Collections.Generic;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.Entity;

namespace PhotoBooth.BL.Facades
{
    public interface ICatalogFacade
    {
        ICollection<RentalItemModel> GetAvailableRentalItems(DateTime since, DateTime till,RentalItemType? type= null);
        ICollection<ProductModel> GetAvailableProducts(DateTime since, DateTime till);
        bool AreAllRentalItemsAvailable(ICollection<RentalItemModel> items, DateTime since, DateTime till);
        ICollection<ItemPackageDTO> GetAllPackages();
    }
}