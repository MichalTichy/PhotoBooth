using AutoMapper;
using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Models;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;
using System.Linq;

namespace PhotoBooth.BL.Queries
{
    public class ItemPackagesQuery : QueryBase<ItemPackage, ItemPackageDTO>
    {
        protected new MapperConfiguration MapConfig = new MapperConfiguration(cfg =>
                cfg.CreateMap<ItemPackage, ItemPackageDTO>()
                .ForMember(dest => dest.RentalItemTypes, act =>
                       act.MapFrom(src => src.RentalItems.Select(x => x.RentalItemType)))
                .ForMember(dest => dest.ProductIds, act =>
                        act.MapFrom(src => src.Products.Select(x => x.ProductId)))
        );

        public ItemPackagesQuery(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }

        protected override System.Linq.IQueryable<ItemPackageDTO> GetQueryable()
        {
            return Context
                .ItemPackages
                .Where(x => x.CurrentlyAvailable)
                .ProjectTo<ItemPackageDTO>(MapConfig);
        }
    }
}