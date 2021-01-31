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
using PhotoBooth.DEMO.API.Filters;

namespace PhotoBooth.DEMO.API.Controllers
{
    [QueryFilter]
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

        [HttpGet("user/{id}")]
        public async Task<ActionResult<OrderSummaryModel>> GetOrdersByUser(Guid id)
        {
            var orders = await Task.Run(() => _orderFacade.GetOrdersByUser(id));
            return Ok(orders);
        }

        [HttpPost()]
        public async Task<ActionResult<OrderSummaryModel>> CreateOrder(OrderSummaryModel order)
        {
            //in case of valid order summary, id will be assigned and the whole order summary will be returned
            var id = order.Id;
            var res = await Task.Run(() => _orderFacade.CreateOrder(order));
            if (res.Id != id)
            {
                return Ok(order);
            }
            else
            {
                return BadRequest(order);
            }
        }

        [HttpPost("edit")]
        public async Task<ActionResult<OrderSummaryModel>> EditOrder(OrderSummaryModel order)
        {
            var success = await Task.Run(() => _orderFacade.UpdateOrder(order));
            if (success)
            {
                return Ok();
            } 
            else
            {
                return BadRequest(order);
            }
        }

        [HttpPost("delete")]
        public async Task<ActionResult<OrderSummaryModel>> DeleteOrder(OrderSummaryModel order)
        {
            var success = await Task.Run(() => _orderFacade.DeleteOrder(order.Id));
            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(order);
            }
        }

        [HttpPost("confirm/{id}")]
        public async Task<ActionResult<OrderSummaryModel>> ConfirmOrder(Guid id)
        {
            var success = await Task.Run(() => _orderFacade.ConfirmOrder(id));
            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(id);
            }
        }

        [HttpPost("cancel/{id}")]
        public async Task<ActionResult<OrderSummaryModel>> CancelOrder(Guid id)
        {
            var success = await Task.Run(() => _orderFacade.CancelOrder(id));
            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest(id);
            }
        }
    }
}
