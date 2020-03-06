using System.Linq;
using Event_Hub_API.Data;
using Event_Hub_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Event_Hub_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext Database;
        public EventsController(ApplicationDbContext database){
            Database = database;
        }

        [HttpGet]
         public IActionResult GetEvents(){
             //                                    Como usar outro método para trazer apenas no nome do clube?
            var eventList = Database.Events.Include(x => x.Club.Name).ToList();
            return Ok(eventList);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id){
            return Ok("Status GET OK \n> Listar evento por ID: "+ id);
        }
        [HttpPost]
        public IActionResult Post([FromBody] Event EventAttribute){

            Event NewEvent = new Event();

            NewEvent.Title = EventAttribute.Title;
            NewEvent.Price = EventAttribute.Price;
            NewEvent.ReleaseDate = EventAttribute.ReleaseDate;
            NewEvent.ClubId = EventAttribute.ClubId;
            NewEvent.Units = EventAttribute.Units;

            Database.Add(NewEvent);
            Database.SaveChanges();

            return Ok(new {info = "Event successfully registered!", events = EventAttribute});
        }
    }
}