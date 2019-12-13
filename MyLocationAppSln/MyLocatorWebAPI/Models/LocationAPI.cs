using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyLocatorWebAPI.Models
{
    public class LocationAPI
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public string Safety { get; set; }
        public string Message { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        [Key]
        public int ID { get; set; }
    }
}
