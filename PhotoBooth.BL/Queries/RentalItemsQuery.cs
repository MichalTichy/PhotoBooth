using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace PhotoBooth.BL.Queries
{
    public class RentalItemsQuery : QueryBase<RentalItem, RentalItemModel>
    {
        public RentalItemsQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override IQueryable<RentalItemModel> GetQueryable()
        {
            return this.Context.Products
                .OrderBy(x => x.Name)
                .ThenBy(x => x.Price)
                .ProjectTo<RentalItemModel>(MapConfig);
        }
    }
}