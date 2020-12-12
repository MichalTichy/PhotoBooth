using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PhotoBooth.BL.Facades;
using PhotoBooth.BL.Models.Item.Product;
using PhotoBooth.BL.Models.Item.RentalItem;
using PhotoBooth.BL.Models.Order;


namespace PhotoBooth.DEMO.API.Controllers
{
    [Route("[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderFacade _orderFacade;


        public OrderController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        [HttpGet("GetOrders/{userId}")]
        public IEnumerable<OrderListModel> GetOrdersByUser(Guid userId)
        {
            return _orderFacade.GetOrdersByUser(userId);
        }

        //[HttpPost("PrepareOrder")]
        //public OrderSummaryModel PrepareOrder(ICollection<RentalItemModel> rentalItems, ICollection<ProductModel> products, OrderMatadata orderMetadata)
        //{
        //    return _orderFacade.PrepareOrder(rentalItems,products,orderMetadata);
        //}
    }
}
