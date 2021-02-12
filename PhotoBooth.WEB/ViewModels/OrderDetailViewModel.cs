using DotVVM.Framework.ViewModel;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Order;
using System;
using System.Threading.Tasks;

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
            Detail = _orderFacade.GetOrderSummaryAsync(OrderId).Result;

            return base.PreRender();
        }

        public void CancelOrder()
        {
            _orderFacade.CancelOrderAsync(Detail.Id);
        }

        public void ConfirmOrder()
        {
            _orderFacade.ConfirmOrder(Detail.Id);
        }
    }
}