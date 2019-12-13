using Microsoft.EntityFrameworkCore;
using MyLocatorWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLocatorWebAPI.Data
{
    public class MyLocatorContext : DbContext
    {
        public MyLocatorContext(DbContextOptions<MyLocatorContext> options)
           : base(options)
        {
        }

        public DbSet<LocationAPI> AllLocationsAPI {get; set; }
    }
}


