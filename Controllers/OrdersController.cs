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
            var allOrders = Database.Orders.Include(x => x.Event).ToList();
            return Ok(allOrders);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id){
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
            NewOrder.EventId = OrderAttribute.EventId;

            Database.Add(NewOrder);
            Database.SaveChanges();

            Response.StatusCode = 201;
            return new ObjectResult(new {info = "Purchase successfully registered!", order = OrderAttribute});
        }
        [Route("price/asc")]
        [HttpGet]
        public IActionResult PriceAsc(){
            var ascendingOrder = Database.Orders.OrderBy(x => x.Price).ToList();
            return Ok(ascendingOrder);
        }

        [Route("price/desc")]
        [HttpGet]
        public IActionResult PriceDesc(){
            var descendingOrder = Database.Orders.OrderByDescending(x => x.Price).ToList();
            return Ok(descendingOrder);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Order OrderId = Database.Orders.First(x => x.Id == id);
                Database.Orders.Remove(OrderId);
                Database.SaveChanges();

                return Ok();
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "id not fount!"});
            }
        }
    }
}