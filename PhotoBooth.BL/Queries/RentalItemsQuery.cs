
using PhotoBooth.DAL.Entity;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Queries
{
    public class RentalItemsQuery : QueryBase<RentalItem, RentalItemModel>
    {
        public RentalItemsQuery(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
