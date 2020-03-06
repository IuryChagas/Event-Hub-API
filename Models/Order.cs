using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Hub_API.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [Range(10.0, 800.0, ErrorMessage = "{0} must be from U$10.00 To $800.00")]
        [Display(Name="Ticket price")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Price { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [Range(1, 200, ErrorMessage = "{0} must be between {1} and {2}")]
        public int Units { get; set; }

        [Required(ErrorMessage = "Event is required.")]
        public int? EventId { get; set; }

        [ForeignKey ("EventId")]
        public virtual Event Event { get; set; }
    }
}