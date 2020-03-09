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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext Database;
        public UsersController(ApplicationDbContext database){
            Database = database;
        }

        [HttpGet]
        public IActionResult GetUsers(){
            var allUsers = Database.Users.ToList();
            return Ok(allUsers);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id){
            try
            {
                var UserById = Database.Users.FirstOrDefault(x => x.Id == id);
                return Ok(UserById);
            }
            catch (Exception)
            {
               return BadRequest("Invalid Id!");
            }

        }
        [HttpPost]
        public IActionResult Post([FromBody] User UserAttribute){

            User NewUser = new User();

            NewUser.Email = UserAttribute.Email;
            NewUser.Password = UserAttribute.Password;

            Database.Add(NewUser);
            Database.SaveChanges();

            Response.StatusCode = 201;
            return new ObjectResult(new {info = "User successfully registered!", user = UserAttribute});
        }
        [Route("asc")]
        [HttpGet]
        public IActionResult OrderByAsc(){
            var ascendingOrder = Database.Users.OrderBy(x => x.Email).ToList();
            return Ok(ascendingOrder);
        }

        [Route("desc")]
        [HttpGet]
        public IActionResult OrderByDesc(){
            var descendingOrder = Database.Users.OrderByDescending(x => x.Email).ToList();
            return Ok(descendingOrder);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                User UserId = Database.Users.First(x => x.Id == id);
                Database.Users.Remove(UserId);
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