using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.Runtime.Filters;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Order;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhotoBooth.WEB.ViewModels
{
    [Authorize]
    public class OrderListViewModel : MasterPageViewModel
    {
        private readonly IOrderFacade _orderFacade;
        private ICollection<OrderListModel> _orders { get; set; }
        public GridViewDataSet<OrderListModel> Orders { get; set; } = new GridViewDataSet<OrderListModel>() {PagingOptions = { PageSize = 15}};

        public OrderListViewModel(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public override Task PreRender()
        {
            var user = Context.GetAuthentication().Context.User;
            if (user.IsInRole("admin"))
            {
                _orders = _orderFacade.GetAllOrdersAsync(true).Result.OrderBy(order => order.State).ToList();
            }
            else
            {
                _orders = _orderFacade.GetOrdersByUserAsync(user.Identity.Name, true).Result;
            }
            Orders.LoadFromQueryable(_orders.AsQueryable());
            return base.PreRender();
        }
    }
}