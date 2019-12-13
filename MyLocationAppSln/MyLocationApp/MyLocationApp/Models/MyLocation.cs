﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MyLocationApp.Models
{
    
    public class MyLocation 
    {

        public string Address { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public string Safety { get; set; }
        public string Message { get; set; }


    } 
}
