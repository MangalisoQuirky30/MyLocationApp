
using Microsoft.EntityFrameworkCore;
using MyLocationApp.Models;
using MyLocationWebApp.Models;


namespace ContosoPets.Api.Data
{
    public class ContosoPetsContext : DbContext
    {
        public ContosoPetsContext(DbContextOptions<ContosoPetsContext> options)
            : base(options)
        {
        }

        public DbSet<MyLocationAPI> Locations { get; set; }
        public DbSet<RegistrationAPI> Users { get; set; }
    }
}

