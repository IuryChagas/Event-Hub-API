using Event_Hub_API.Data;
using Event_Hub_API.Models;
using Microsoft.AspNetCore.Mvc;

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
            return Ok(new {name = "Nome do usuário", informations = "outras informações"});
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id){
            return Ok("Status GET OK \n> Listar usuário por ID: "+ id);
        }
        [HttpPost]
        public IActionResult Post([FromBody] User UserAttribute){

            User NewUser = new User();

            NewUser.Email = UserAttribute.Email;
            NewUser.Password = UserAttribute.Password;

            Database.Add(NewUser);
            Database.SaveChanges();

            return Ok(new {info = "Usuário registrado com sucesso!", user = UserAttribute});
        }
    }
}