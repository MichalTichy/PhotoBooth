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
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderFacade _orderFacade;


        public OrdersController(IOrderFacade orderFacade)
        {
            _orderFacade = orderFacade;
        }

        public async Task<ActionResult<OrderListModel>> Get()
        {
            //API ready to work asynchronously when async methods are implemented in lower levels
            var orders = await Task.Run(() => _orderFacade.GetAllOrders());
            return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderSummaryModel>> GetOrderById(Guid id)
        {
            var order = await Task.Run(() => _orderFacade.GetOrderById(id));
            if(order == null)
            {
                BadRequest();
            }
            return Ok(order);
        }
    }
}
