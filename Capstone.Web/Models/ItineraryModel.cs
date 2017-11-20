using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

    }
namespace Capstone.Web.Models
{
    public class ItineraryModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string StartLocation { get; set; }
        public DateTime Date { get; set; }
}