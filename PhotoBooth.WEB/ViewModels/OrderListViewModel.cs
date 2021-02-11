using DotVVM.Framework.Hosting;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhotoBooth.WEB.ViewModels
{
    public class OrderListViewModel : MasterPageViewModel
    {
        private readonly IOrderFacade _orderFacade;
        public ICollection<OrderListModel> Orders { get; set; }

        public OrderListViewModel(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public override Task PreRender()
        {
            var user = Context.GetAuthentication().Context.User;
            if (user.Identity.Name == "admin")
            {
                Orders = _orderFacade.GetAllOrdersAsync(true).Result;
            }
            else
            {
                Orders = _orderFacade.GetOrdersByUserAsync(user.Identity.Name).Result;
            }
            return base.PreRender();
        }
    }
}