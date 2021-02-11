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
using System.Threading.Tasks;

namespace PhotoBooth.BL.Facades
{
    public class CatalogFacade : FacadeBase<Product>, ICatalogFacade
    {
        public CatalogFacade(BaseRepository<Product> repository, IUnitOfWorkProvider uow) : base(repository, uow)
        {
        }

        public async Task<bool> AreAllRentalItemsAvailableAsync(ICollection<RentalItemModel> items, DateTime since, DateTime till)
        {
            using (UnitOfWorkFactory.Create())
            {
                var available = await new AvailableRentalItems(UnitOfWorkFactory, since, till).ExecuteAsync();
                return items.All(x => available.Contains(x));
            }
        }

        public async Task<ICollection<ItemPackageDTO>> GetAllPackagesAsync()
        {
            using (UnitOfWorkFactory.Create())
            {
                return await new ItemPackagesQuery(UnitOfWorkFactory).ExecuteAsync();
            }
        }

        public async Task<ICollection<ProductModel>> GetAvailableProductsAsync()
        {
            using (var uow = UnitOfWorkFactory.Create())
            {
                var query = new AvailableProductsQuery(UnitOfWorkFactory);
                return await query.ExecuteAsync();
            }
        }

        public async Task<ICollection<RentalItemModel>> GetAvailableRentalItemsAsync(DateTime since, DateTime till, RentalItemType? type = null)
        {
            using (UnitOfWorkFactory.Create())
            {
                var query = new AvailableRentalItems(UnitOfWorkFactory, since, till, type);
                return await query.ExecuteAsync();
            }
        }
    }
}