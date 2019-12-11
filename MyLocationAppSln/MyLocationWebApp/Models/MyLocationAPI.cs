using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace MyLocationWebApp.Models
{
    public class MyLocationAPI
    {
        public string Address { get; set; }
        public string Description { get; set; }

        [System.ComponentModel.DataAnnotations.Key]
        public int UserId { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}


