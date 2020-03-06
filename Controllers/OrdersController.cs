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
            var OrderList = Database.Orders.Include(x => x.Event).ToList();
            return Ok(OrderList);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id){
            return Ok("Status GET OK \n> Listar venda por ID: "+ id);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Order OrderAttribute){

            Order NewOrder = new Order();

            NewOrder.Price = OrderAttribute.Price;
            NewOrder.Units = OrderAttribute.Units;
            NewOrder.EventId = OrderAttribute.EventId;

            Database.Add(NewOrder);
            Database.SaveChanges();

            return Ok(new {info = "Compra registrada com sucesso!", order = OrderAttribute});
        }
    }
}