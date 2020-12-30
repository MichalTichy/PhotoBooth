using System;
using System.Collections.Generic;
using System.Text;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFrameworkCore;
using PhotoBooth.DAL;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Linq;

namespace PhotoBooth.BL.Queries
{
    public abstract class QueryBase<TStart, TResult> : EntityFrameworkQuery<TResult,PhotoBoothContext>
    {
        protected MapperConfiguration MapConfig = new MapperConfiguration(cfg =>
                cfg.CreateMap<TStart, TResult>());
        public new PhotoBoothContext Context => (PhotoBoothContext)base.Context;
        public QueryBase(IUnitOfWorkProvider unitOfWorkProvider) : base(unitOfWorkProvider)
        {
        }
    }
}

