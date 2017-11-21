using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Capstone.Web.Models
{
    public class PlacesModel
    {
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public int? GoogleID { get; set; }
        public string Detail { get; set; }
        public string PlaceName { get; set; }
        public string Category { get; set; }
        public int Zip { get; set; }

    }
}