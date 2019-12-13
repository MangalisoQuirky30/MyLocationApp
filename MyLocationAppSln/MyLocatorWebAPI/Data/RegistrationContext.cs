using Microsoft.EntityFrameworkCore;
using MyLocatorWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLocatorWebAPI.Data
{
    public class RegistrationContext : DbContext
    {
        public RegistrationContext(DbContextOptions<RegistrationContext> options)
           : base(options)
        {
        }

        public DbSet<RegistrationAPI> RegistrationAPIUsers { get; set; }
    }
}


