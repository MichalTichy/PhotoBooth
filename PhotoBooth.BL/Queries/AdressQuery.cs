using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace PhotoBooth.BL.Queries
{
    public class AddressQuery : QueryBase<Address, AddressModel>
    {
        public AddressQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<AddressModel> GetQueryable()
        {
            return this.Context.Addresses
                .ProjectTo<AddressModel>(MapConfig);
        }
    }
}