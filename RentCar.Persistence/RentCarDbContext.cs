using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence
{
    public class RentCarDbContext : DbContext
    {
        public RentCarDbContext(DbContextOptions<RentCarDbContext> options)
            : base(options) { }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Client> Clients { get; set; }
        //public DbSet<Business> Businesses { get; set; }
        //public DbSet<Car> Cars { get; set; }
        //public DbSet<Reservation> Reservations { get; set; }
        //public DbSet<Contract> Contracts { get; set; }
        //public DbSet<CarPricingRule> CarPricingRules { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Fluent API configuration (optional for now)
        }
    }

}
