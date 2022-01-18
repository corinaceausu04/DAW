using DAW.Data.Abstractions;
using DAW.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAW.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderManager _orderManager;

        public OrderController(IOrderManager orderManager)
        {
            _orderManager = orderManager;
        }

        [HttpPost("CreateOrder")]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var res = await _orderManager.CreateOrder(order, order.Items);
            return res;
        }

        [HttpGet("GetOrder/{id}")]
        public ActionResult<Order> GetOrderById(int orderId)
        {
            var order = _orderManager.GetOrderById(orderId);

            if (order == null)
                return BadRequest("Order was not found!");
            return order;
        }
        public IActionResult DeleteOrder(int orderId)
        {
            var ord = _orderManager.GetOrderById(orderId);
            if (ord == null)
                return BadRequest("The order does not exist.");
            _orderManager.DeteleOrder(ord);

            return Ok();
        }
    }
}
