﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    public class ItineraryPlacesModel
    {
        public List<PlacesModel> Places { get; set; }
        public ItineraryModel Itinerary { get; set; }

    }
}