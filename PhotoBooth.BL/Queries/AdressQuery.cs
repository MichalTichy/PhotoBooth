using Riganti.Utils.Infrastructure.Core;
using System.Linq;
using PhotoBooth.BL.Models.Address;
using PhotoBooth.DAL.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;

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
