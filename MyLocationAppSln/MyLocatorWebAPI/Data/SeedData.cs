using MyLocatorWebAPI.Models;

using MyLocatorWebAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLocatorWebAPI.Data
{
    public class SeedData
    {
        public static void Initialize(MyLocatorContext context , RegistrationContext regContext)
        {
            if (!regContext.RegistrationAPIUsers.Any())
            {
                regContext.RegistrationAPIUsers.AddRange(
                    new RegistrationAPI
                    {
                        userEmail = "mango@gmail.com",
                        userName = "Mangaliso Sereo" ,
                        userPassword = "11111"
                    },

                    new RegistrationAPI
                    {
                        userEmail = "sereo@gmail.com",
                        userName = "Sereo",
                        userPassword = "11111"
                    }
                );

                regContext.SaveChanges();
            }


            if (!context.AllLocationsAPI.Any())
            {
                context.AllLocationsAPI.AddRange(
                    new LocationAPI
                    {
                        Address = "Squeaky Bone",
                        Description = "ghjkf",
                        Latitude = -33.94374,
                        Longitude = 18.5302,
                        ID = 2
                    },

                    new LocationAPI
                    {
                        Address = "Squeaky Bone",
                        Description = "ghjkf",
                        Longitude = -33.94384,
                        Latitude = 18.5309,
                        ID = 1
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
