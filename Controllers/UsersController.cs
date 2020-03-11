using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Event_Hub_API.Data;
using Event_Hub_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var usId = Database.Users.FirstOrDefault();
            if (usId == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "User not found!"});
            }

            var AllUsers = Database.Users.Select(x => new{x.Id, x.Email} ).ToList();
            return Ok(AllUsers);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id){
            var usId = Database.Users.FirstOrDefault();
            if (usId == null)
            {
                Response.StatusCode = 404;
                return NotFound(new {info = "User not found!"});
            }
            var UserById = Database.Users.Select(x => new{x.Id, x.Email} ).FirstOrDefault(x => x.Id == id);

            if (UserById == null)
            {
                return NotFound();
            }
            return Ok (UserById);

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
        [HttpPost("Login")]
        public IActionResult Login([FromBody] User  credentials){
            try
            {
                User user = Database.Users.First(user => user.Email.Equals(credentials.Email));
                if (user != null)
                {
                    if(user.Password.Equals(credentials.Password)){

                        string SecurityKey = "segurity_key__token";
                        var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));

                        var accessCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256Signature);

                        var JWT = new JwtSecurityToken(
                            issuer: "EventHub API",
                            expires: DateTime.Now.AddHours(1),
                            audience: "users_admin",
                            signingCredentials: accessCredentials
                        );

                        return Ok (new JwtSecurityTokenHandler().WriteToken(JWT));
                        
                    }else{
                        Response.StatusCode = 401;
                        return new ObjectResult(new {info = "Error Password >>> Unauthorized!"});
                    }
                } else
                {
                    Response.StatusCode = 401;
                    return new ObjectResult(new {info = "Error Password >>> Unauthorized!"});
                }
            }
            catch (System.Exception)
            {
                    Response.StatusCode = 404;
                    return new ObjectResult(new {info = "Email not Found >>> Unauthorized!"});
            }
        }

    }
}