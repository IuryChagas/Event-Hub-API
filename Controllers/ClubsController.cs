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
    public class ClubsController : ControllerBase
    {
        private readonly ApplicationDbContext Database;
        public ClubsController(ApplicationDbContext database){
            Database = database;
        }

        [HttpGet]
        public IActionResult GetClubs(){
            var clubId = Database.Clubs.FirstOrDefault();
            if (clubId == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "no registered club!"});
            }
            var allClubs = Database.Clubs.ToList();
            return Ok(allClubs);
        }

        [Route("name/asc")]
        [HttpGet]
        public IActionResult NameAsc(){
            var clubId = Database.Clubs.FirstOrDefault();
            if (clubId == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "no registered club!"});
            }

            var ascendingOrder = Database.Clubs.OrderBy(x => x.Name).ToList();
            return Ok(ascendingOrder);
        }

        [Route("name/desc")]
        [HttpGet]
        public IActionResult NameDesc(){
            var clubId = Database.Clubs.FirstOrDefault();
            if (clubId == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "no registered club!"});
            }

            var descendingOrder = Database.Clubs.OrderByDescending(x => x.Name).ToList();
            return Ok(descendingOrder);
        }

        [Route("maximumcapacity/asc")]
        [HttpGet]
        public IActionResult CapacityAsc(){
            var clubId = Database.Clubs.FirstOrDefault();
            if (clubId == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "no registered club!"});
            }

            var ascendingOrder = Database.Clubs.OrderBy(x => x.MaximumCapacity).ToList();
            return Ok(ascendingOrder);
        }

        [Route("maximumcapacity/desc")]
        [HttpGet]
        public IActionResult CapacityDesc(){
            var clubId = Database.Clubs.FirstOrDefault();
            if (clubId == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "no registered club!"});
            }

            var descendingOrder = Database.Clubs.OrderByDescending(x => x.MaximumCapacity).ToList();
            return Ok(descendingOrder);
        }

        [Route("name/{queryName}")]
        [HttpGet]
        public IActionResult GetByName(string queryName){
            var clubId = Database.Clubs.FirstOrDefault();
            if (clubId == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "no registered club!"});
            }

            var ClubSearched = Database.Clubs.Where(x => x.Name.Contains(queryName, StringComparison.InvariantCultureIgnoreCase)).ToList();

            if(!ClubSearched.Any()){
                return NotFound();
            }
            return Ok(ClubSearched);
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id){
            var isValid = Database.Clubs.FirstOrDefault();
            if (isValid == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "no registered club!"});
            }

            var ClubId = Database.Clubs.FirstOrDefault(x => x.Id == id);

            if (ClubId == null)
            {
                return new NotFoundObjectResult(new { msg = "invalid id!"});
            }

            return Ok(ClubId);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Club ClubAttribute){

            if (ModelState.IsValid)
            {
                Club NewClub = new Club();

                NewClub.Name = ClubAttribute.Name;
                NewClub.Street = ClubAttribute.Street;
                NewClub.Number = ClubAttribute.Number;
                NewClub.ZipCode = ClubAttribute.ZipCode;
                NewClub.City = ClubAttribute.City;
                NewClub.MaximumCapacity = ClubAttribute.MaximumCapacity;
                Database.Add(NewClub);
                Database.SaveChanges();

                Response.StatusCode = 201;
                return new ObjectResult(new {info = "Club successfully registered!", club = ClubAttribute});
            }
            Response.StatusCode = 400;
            return new ObjectResult(new {msg = ModelState.Values.SelectMany(v => v.Errors).Select(v => v.ErrorMessage).ToList()});
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Club club)
        {
            if(club.Id > 0){
                try
                {
                    if (id != club.Id)
                    {
                        throw new Exception("invalid id!");
                    }
                    var clubId = Database.Clubs.First(changeClub => changeClub.Id == club.Id);
                    if (clubId != null)
                    {
                        clubId.Name = club.Name != null ? club.Name : clubId.Name;
                        clubId.Street = club.Street != null ? club.Street : clubId.Street;
                        clubId.Number = club.Number != 0 ? club.Number : clubId.Number;
                        clubId.ZipCode = club.ZipCode != null ? club.ZipCode : clubId.ZipCode;
                        clubId.City = club.City != null ? club.City : clubId.City;
                        clubId.MaximumCapacity = club.MaximumCapacity != 0 ? club.MaximumCapacity : clubId.MaximumCapacity;

                        Database.SaveChanges();
                        Response.StatusCode = 200;
                        return new ObjectResult(new {msg = "Successful change!"});

                    }else {
                        Response.StatusCode = 400;
                        return new ObjectResult(new {msg = "Club not found!"});
                    }
                }
                catch
                {
                    Response.StatusCode = 404;
                    return NotFound(new {msg = "Club not found!"});
                }
            }else{
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "invalid id!"});
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                Club ClubId = Database.Clubs.First(x => x.Id == id);
                Database.Clubs.Remove(ClubId);
                Database.SaveChanges();

                return new ObjectResult(new {msg = "Club deleted successfully!"});
            }
            catch (Exception)
            {
                Response.StatusCode = 404;
                return new ObjectResult(new {msg = "id not fount!"});
            }
        }
    }
}