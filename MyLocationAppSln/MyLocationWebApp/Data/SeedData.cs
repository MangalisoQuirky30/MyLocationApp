
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyLocationWebApp.Models;

namespace ContosoPets.Api.Data
{
    public static class SeedData
    {
        public static void Initialize(ContosoPetsContext context)
        {
            if (!context.Locations.Any())
            {
                context.Locations.AddRange(
                    new MyLocationAPI
                    {
                        Address = "Male",
                        Description = "red",
                    },
                    new MyLocationAPI
                    {
                        Address = "Male",
                        Description = "red"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}


