
using System.Linq;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.DAL.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Queries
{
    public class AddressQuery : QueryBase<Address, AddressModel>
    {
        public AddressQuery(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
