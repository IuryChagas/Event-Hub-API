using System;
using System.Linq;
using Event_Hub_API.Data;
using Event_Hub_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_Hub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly ApplicationDbContext Database;
        public OrdersController(ApplicationDbContext database){
            Database = database;
        }

        [HttpGet]
        public IActionResult GetOrders(){
            var OrderId = Database.Orders.FirstOrDefault();
            if (OrderId == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "Order not found!"});
            }

            var allOrders = Database.Orders.ToList();
            return Ok(allOrders);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id){
            var orderId = Database.Orders.FirstOrDefault();
            if (orderId == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "Order not found!"});
            }

            try
            {
                var OrderId = Database.Orders.FirstOrDefault(x => x.Id == id);
                return Ok(OrderId);
            }
            catch (Exception)
            {
                return BadRequest(new {msg = "invalid Id!"});
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] Order OrderAttribute){

                Order NewOrder = new Order();

                NewOrder.Price = OrderAttribute.Price;
                NewOrder.Units = OrderAttribute.Units;
                NewOrder.EventName = OrderAttribute.EventName;

                Database.Add(NewOrder);
                Database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult(new {info = "Purchase successfully registered!", order = OrderAttribute});

        }

    }
}