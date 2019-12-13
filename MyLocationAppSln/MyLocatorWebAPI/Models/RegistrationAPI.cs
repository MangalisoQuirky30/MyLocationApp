using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLocatorWebAPI.Models
{
    public class RegistrationAPI
    {
        public string userName { get; set; }
        public string userEmail { get; set; }
        public string userPhoneNumber { get; set; }
        public string userPassword { get; set; }
        public string userConfirmPassword { get; set; }
        public int userID { get; set; }
        public int ID { get; set; }
    }
}
