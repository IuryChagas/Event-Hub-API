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
    public class EventsController : ControllerBase
    {
        private readonly ApplicationDbContext Database;
        public EventsController(ApplicationDbContext database){
            Database = database;
        }

        [HttpGet]
         public IActionResult GetEvents(){
             //                                    Como usar outro método para trazer apenas no nome do clube?
            var allEvents = Database.Events.Include(x => x.Club).ToList();
            return Ok(allEvents);
        }

        [Route("title/asc")]
        [HttpGet]
        public IActionResult OrderByTitleAsc(){
            var ascendingOrder = Database.Events.OrderBy(x => x.Title).ToList();
            return Ok(ascendingOrder);
        }

        [Route("title/desc")]
        [HttpGet]
        public IActionResult OrderByTitleDesc(){
            var descendingOrder = Database.Events.OrderByDescending(x => x.Title).ToList();
            return Ok(descendingOrder);
        }

        [Route("releasedata/asc")]
        [HttpGet]
        public IActionResult OrderDateByAsc(){
            var ascendingOrder = Database.Events.OrderBy(x => x.ReleaseDate).ToList();
            return Ok(ascendingOrder);
        }

        [Route("releasedata/desc")]
        [HttpGet]
        public IActionResult OrderDateByDesc(){
            var descendingOrder = Database.Events.OrderByDescending(x => x.ReleaseDate).ToList();
            return Ok(descendingOrder);
        }

        [Route("title/asc")]
        [HttpGet]
        public IActionResult OrderTitleByAsc(){
            var ascendingOrder = Database.Events.OrderBy(x => x.Title).ToList();
            return Ok(ascendingOrder);
        }

        [Route("title/desc")]
        [HttpGet]
        public IActionResult OrderTitleByDesc(){
            var descendingOrder = Database.Events.OrderByDescending(x => x.Title).ToList();
            return Ok(descendingOrder);
        }

        [Route("price/asc")]
        [HttpGet]
        public IActionResult OrderByAsc(){
            var ascendingOrder = Database.Events.OrderBy(x => x.Price).ToList();
            return Ok(ascendingOrder);
        }

        [Route("price/desc")]
        [HttpGet]
        public IActionResult OrderByDesc(){
            var descendingOrder = Database.Events.OrderByDescending(x => x.Price).ToList();
            return Ok(descendingOrder);
        }

        [Route("name/{queryName}")]
        [HttpGet]
        public IActionResult GetByName(string queryName){
            var EventSearched = Database.Events.Where(x => x.Title.Contains(queryName, StringComparison.InvariantCultureIgnoreCase)).ToList();

            if(EventSearched == null){
                return NotFound();
            }
            return Ok(EventSearched);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id){
            try
            {
                var EventId = Database.Events.First(x => x.Id == id);
                return Ok(EventId);
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "id not fount!"});
            }
            
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Event events)
        {
            if(events.Id > 0){
                try
                {
                    if (id != events.Id)
                    {
                        throw new Exception("invalid id!");
                    }
                    var eventId = Database.Events.First(changeEvent => changeEvent.Id == events.Id);
                    if (eventId != null)
                    {
                        eventId.Title = events.Title != null ? events.Title : eventId.Title;
                        eventId.Price = events.Price != 0 ? events.Price : eventId.Price;
                        eventId.ReleaseDate = events.ReleaseDate != null ? events.ReleaseDate : eventId.ReleaseDate;
                        eventId.Club = events.Club != null ? events.Club : eventId.Club;
                        eventId.Units = events.Units != 0 ? events.Units : eventId.Units;

                        Database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult("");

                    }else {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Event not found!"});
                    }
                }
                catch
                {
                    return new ObjectResult(new {msg = "Event not found!"});
                }
            }else{
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "invalid id!"});
            }
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

            Response.StatusCode = 201;
            return new ObjectResult(new {info = "Event successfully registered!", events = EventAttribute});
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Event EventId = Database.Events.First(x => x.Id == id);
                Database.Events.Remove(EventId);
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