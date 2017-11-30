using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

    
namespace Capstone.Web.Models
{
    public class ItineraryModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required(ErrorMessage = "Please name your itinerary.")]
        public string Name { get; set; }

        [Display(Name = "Starting Location")]
        [Required(ErrorMessage = "Please select a starting location.")]
        public int StartingLocationId { get; set; }

        public string StartLocationLat { get; set; }

        public string StartLocationLong { get; set; }

        [Required(ErrorMessage = "Please select a starting date.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }

}

