using PhotoBooth.BL.Facades;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Queries
{
    public class ItemPackagesQuery : QueryBase<ItemPackage, ItemPackageDTO>
    {
        public ItemPackagesQuery(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
