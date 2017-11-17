using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Capstone.Web.Models;

namespace Capstone.Web.Classes
{
    public class Places
    {
        private List<PlacesModel> places = new List<PlacesModel>();
        public Places(List<PlacesModel> places)
        {
            this.places = places;
        }

        public List<PlacesModel> List { get; set; }
    }
}