using System.ComponentModel.DataAnnotations;

namespace Event_Hub_API.Models
{
    public class Club
    {
public int Id { get; set; }

        [Required(ErrorMessage = "A name is required.")]
        [StringLength(30,ErrorMessage = "Name is very long")]
        [MinLength(3,ErrorMessage = "Name is too short")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Street is required.")]
        [MinLength(1,ErrorMessage = "null is not accepted.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Number is required.")]
        [Range(1, int.MaxValue,ErrorMessage = "null is not accepted.")]
        public int Number { get; set; }

        [Required(ErrorMessage = "Zip Code is required.")]
        [MinLength (8, ErrorMessage = "Zip Code must to be minimum 8 digits")]
        public string ZipCode { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [MinLength (5, ErrorMessage = "City must to be minimum 5 letters")]
        [StringLength(40,ErrorMessage = "City is very long")]
        public string City { get; set; }

        [Required(ErrorMessage = "Maximum Capacity is required.")]
        [Range(200.0, 10000.0, ErrorMessage = "{0} must to be between {1} and {2}")]
        public int MaximumCapacity { get; set; }
    }
}