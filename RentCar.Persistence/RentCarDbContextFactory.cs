using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentCar.Persistence
{
    public class RentCarDbContextFactory : IDesignTimeDbContextFactory<RentCarDbContext>
    {
        public RentCarDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<RentCarDbContext>();
             
            optionsBuilder.UseSqlServer(
                "Server=ASULEJMANI\\SQLEXPRESS;Database=HRMSH_TEST;Trusted_Connection=True;TrustServerCertificate=True;");

            return new RentCarDbContext(optionsBuilder.Options);
        }
    }
}
