using Riganti.Utils.Infrastructure.Core;
using System.Linq;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.DAL.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using PhotoBooth.DAL.UnitOfWork;
using PhotoBooth.BL.Models.Item.RentalItem;

namespace PhotoBooth.BL.Queries
{
    public class AddressQuery : QueryBase<Address, AddressModel>
    {
        public AddressQuery(string dbName = "") : base(dbName) { }

    }
}
