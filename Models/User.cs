using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Event_Hub_API.Models
{
    [DataContract]
    public class User
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A email is required.")]
        [StringLength(30,ErrorMessage = "email is very long")]
        [MinLength(3,ErrorMessage = "email is too short")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [MinLength(4,ErrorMessage = "Your password is too short")]
        [StringLength(8,ErrorMessage = "Your password is very long")]
        [JsonIgnore]
        public string Password { get; set; }

    }
}