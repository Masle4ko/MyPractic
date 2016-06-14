using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyBlog.Models
{
    public class Map
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string PlaceName { get; set; } 
        public int Price { get; set; } 
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string GeoLong { get; set; }
        public string GeoLat { get; set; } 
    }
}