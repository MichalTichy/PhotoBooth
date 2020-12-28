using Riganti.Utils.Infrastructure.Core;
using System.Linq;
using PhotoBooth.BL.Models.Order;
using PhotoBooth.DAL.Entity;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace PhotoBooth.BL.Queries
{
    public class OrderListQuery : QueryBase<Order, OrderListModel>
    {
        public OrderListQuery(string dbName = "") : base(dbName)
        {
        }

    }
}
