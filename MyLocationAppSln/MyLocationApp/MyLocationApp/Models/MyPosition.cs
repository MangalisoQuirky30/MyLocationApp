using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;

namespace MyLocationApp.Models
{
    public class Location
    {
        public string Address { get; set; }
        public string Description { get; set; }
        public string Safety { get; set; }
        public string Message { get; set; }
        public Position Position { get; set; }
        public Location(string address, string description, Position position)
        {
            Address = address;
            Description = description;
            Position = position;
        }

    }
}
