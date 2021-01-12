using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Order;

namespace PhotoBooth.WEB.ViewModels
{
    public class OrderDetailViewModel : MasterPageViewModel
    {
        private readonly IOrderFacade _orderFacade;
        public OrderSummaryModel Detail { get; set; }

        [FromRoute("id")]
        public Guid OrderId { get; set; }
        public OrderDetailViewModel(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public override Task PreRender()
        {
            Detail = _orderFacade.GetOrderSummary(OrderId);

            return base.PreRender();
        }

        public void Cancel()
        {
            _orderFacade.CancelOrder(Detail.Id);
        }

        public void Confirm()
        {
            _orderFacade.ConfirmOrder(Detail.Id);
        }
    }
}

