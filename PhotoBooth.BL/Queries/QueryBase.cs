using AutoMapper;
using PhotoBooth.DAL;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using System;

namespace PhotoBooth.BL.Queries
{
    public abstract class QueryBase<TStart, TResult> : EntityFrameworkQuery<TResult, PhotoBoothContext>
    {
        protected MapperConfiguration MapConfig => MapConfigInternal.Value;
        protected Lazy<MapperConfiguration> MapConfigInternal;

        private MapperConfiguration CreateMapper()
        {
            return new MapperConfiguration(cfg => CreateMap(cfg));
        }

        protected virtual void CreateMap(IMapperConfigurationExpression cfg)
        {
        }


        public QueryBase(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
            MapConfigInternal = new Lazy<MapperConfiguration>(CreateMapper);
        }
    }
}

