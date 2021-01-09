using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace PhotoBooth.BL.Queries
{
    internal interface IQuery<TStart, TResult>
    {
        void Where(Expression<Func<TStart, bool>> predicate);
        public void SortBy(Func<IQueryable<TStart>, IOrderedQueryable<TStart>> sortAccordingTo, bool ascedingOrder);
        void Page(int pageToFetch, int pageSize);
        ICollection<TResult> ExecuteAsync();
    }
}
