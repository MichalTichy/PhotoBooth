
using PhotoBooth.BL.Models.Address;
using PhotoBooth.DAL.Entity;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Queries
{
    public class AddressQuery : QueryBase<Address, AddressModel>
    {
        public AddressQuery(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
