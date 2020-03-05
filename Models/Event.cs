using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Event_Hub_API.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "A title is required.")]
        [StringLength(30,ErrorMessage = "title is very long")]
        [MinLength(3,ErrorMessage = "title is too short")]
        public string Title { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [Range(10.0, 800.0, ErrorMessage = "{0} must be from U$10.00 To $800.00")]
        [Display(Name="Ticket price")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Price { get; set; }

        [Required(ErrorMessage = "The {0} value is mandatory")]
        [DataType(DataType.Date)]
        [Display(Name="Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ReleaseDate { get; set; }
        
        [Required(ErrorMessage = "Club is required.")]
        public int? ClubId { get; set; }
        
        [ForeignKey ("ClubId")]
        public virtual Club Club { get; set; }

        [Required(ErrorMessage = "{0} required.")]
        [Range(150, 2000, ErrorMessage = "{0} must be between {1} and {2}")]
        public int Units { get; set; }
    }
}