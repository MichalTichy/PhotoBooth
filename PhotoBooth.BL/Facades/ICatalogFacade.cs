using PhotoBooth.BL.Models;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoBooth.BL.Facades
{
    public interface ICatalogFacade
    {
        Task<ICollection<RentalItemModel>> GetAvailableRentalItemsAsync(DateTime since, DateTime till, RentalItemType? type = null);

        Task<ICollection<ProductModel>> GetAvailableProductsAsync();

        Task<bool> AreAllRentalItemsAvailableAsync(ICollection<RentalItemModel> items, DateTime since, DateTime till);

        Task<ICollection<ItemPackageDTO>> GetAllPackagesAsync();
    }
}