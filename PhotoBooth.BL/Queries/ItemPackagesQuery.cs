
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using PhotoBooth.BL.Facades;
using PhotoBooth.DAL.Entity;
using Riganti.Utils.Infrastructure.Core;

namespace PhotoBooth.BL.Queries
{
    public class ItemPackagesQuery : QueryBase<ItemPackage, ItemPackageDTO>
    {
        protected new MapperConfiguration MapConfig = new MapperConfiguration(cfg =>
                cfg.CreateMap<ItemPackage, ItemPackageDTO>()
                .ForMember( dest => dest.RentalItemTypes, act => 
                        act.MapFrom(src => src.RentalItems.Select(x => x.Type)))
                .ForMember(dest => dest.ProductIds, act => 
                        act.MapFrom(src => src.Products.Select(x => x.Id)))
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
