using System;
using System.Collections.Generic;
using System.Text;
using PhotoBooth.DAL;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using System.Linq;
using PhotoBooth.DAL.UnitOfWork;
using PhotoBooth.DAL.Entity;
using System.Linq.Expressions;
using PhotoBooth.BL.Models.Item.RentalItem;

namespace PhotoBooth.BL.Queries
{
    public abstract class QueryBase<TStart, TResult> : IQuery<TStart, TResult> where TStart : class, IEntity, new()
    {
        protected string specialDatabaseLink = "";
        protected int pageSize { get; set; } = 10;
        protected int desiredPage { get; set; } = 1;
        protected Func<IQueryable<TStart>, IOrderedQueryable<TStart>> sortLambda { get; set; }
        protected bool useAscendedOrder { get; set; } = true;
        protected Expression<Func<TStart, bool>> fPredicate { get; set; }

        protected MapperConfiguration MapConfig = new MapperConfiguration(cfg =>
                cfg.CreateMap<TStart, TResult>());

        public QueryBase(string databaseName = "")
        {
            specialDatabaseLink = databaseName;
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
            using (var uow = new UnitOfWork())
            {
                IQueryable temp = uow.GetRepo<TStart>().Get(fPredicate, sortLambda, "").Take(pageSize).AsQueryable();
                return (ICollection<TResult>)temp.ProjectTo<TResult>(MapConfig);
            }
        }
    
    }
}

