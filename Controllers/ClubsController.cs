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
            var ClubList = Database.Clubs.ToList();
            return Ok(ClubList);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id){
            return Ok("Status GET OK \n> Listar por ID: "+ id);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Club ClubAttribute){

            Club NewClub = new Club();

            NewClub.Name = ClubAttribute.Name;
            NewClub.Street = ClubAttribute.Street;    
            NewClub.Number = ClubAttribute.Number;
            NewClub.ZipCode = ClubAttribute.ZipCode;
            NewClub.City = ClubAttribute.City;
            NewClub.MaximumCapacity = ClubAttribute.MaximumCapacity;

            Database.Add(NewClub);
            Database.SaveChanges();

            return Ok(new {info = "Club registrado com sucesso!", club = ClubAttribute});
        }
    }
}