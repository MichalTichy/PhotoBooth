using System;
using System.Collections.Generic;
using System.Text;
using PhotoBooth.DAL;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Linq;
using PhotoBooth.DAL.Entity;
using System.Linq.Expressions;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.DAL.UnitOfWorkProviderModels;

namespace PhotoBooth.BL.Queries
{
    public abstract class QueryBase<TStart, TResult> : IQuery<TStart, TResult> where TStart : class, IEntity, new()
    {
        protected IUnitOfWorkProvider provider;
        protected int pageSize { get; set; } = 10;
        protected int desiredPage { get; set; } = 1;
        protected Func<IQueryable<TStart>, IOrderedQueryable<TStart>> sortLambda { get; set; }
        protected bool useAscendedOrder { get; set; } = true;
        protected Expression<Func<TStart, bool>> fPredicate { get; set; }

        protected MapperConfiguration MapConfig = new MapperConfiguration(cfg =>
                cfg.CreateMap<TStart, TResult>());

        public QueryBase(IUnitOfWorkProvider provider)
        {
            this.provider = provider;
        }

        public void Where(Expression<Func<TStart, bool>> predicate)
        {
            fPredicate = predicate;
        }

        public void SortBy(Func<IQueryable<TStart>, IOrderedQueryable<TStart>> sortAccordingTo, bool ascedingOrder)
        {
            useAscendedOrder = ascedingOrder;
            sortLambda = sortAccordingTo;
        }

        public void Page(int pageToFetch, int pageSize)
        {
            this.pageSize = pageSize;
            desiredPage = pageToFetch;
        }

        public virtual ICollection<TResult> ExecuteAsync()
        {
            using (var uow = provider.GetUinOfWork())
            {
                IQueryable temp = uow.GetRepo<TStart>().Get(fPredicate, sortLambda, "").Take(pageSize).ToList().AsQueryable();
                return (ICollection<TResult>)temp.ProjectTo<TResult>(MapConfig).ToList();
            }
        }
    
    }
}

