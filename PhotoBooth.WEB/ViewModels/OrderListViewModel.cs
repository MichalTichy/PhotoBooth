using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ViewModel;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Order;

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
            if (user.Identity.Name=="admin")
            {
                Orders = _orderFacade.GetAllOrders(true);

            }
            else
            {
                Orders = _orderFacade.GetOrdersByUser(user.Identity.Name);

            }
            return base.PreRender();
        }
    }
}

