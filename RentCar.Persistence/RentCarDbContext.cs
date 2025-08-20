using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RentCar.Domain.Entities;

namespace RentCar.Persistence
{
    public class RentCarDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public RentCarDbContext(DbContextOptions<RentCarDbContext> options)
            : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatus> ReservationStatuses { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<CarPricingRule> CarPricingRules { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<BusinessLocation> BusinessLocations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One-to-One User Relations
            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithOne(u => u.Client)
                .HasForeignKey<Client>(c => c.UserId);

            modelBuilder.Entity<Business>()
                .HasOne(b => b.User)
                .WithOne(u => u.Business)
                .HasForeignKey<Business>(b => b.UserId);

            // Car
            modelBuilder.Entity<Car>()
                .Property(c => c.DailyPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarModel)
                .WithMany()
                .HasForeignKey(c => c.CarModelId);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarType)
                .WithMany()
                .HasForeignKey(c => c.CarTypeId);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.FuelType)
                .WithMany()
                .HasForeignKey(c => c.FuelTypeId);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Transmission)
                .WithMany()
                .HasForeignKey(c => c.TransmissionId);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Business)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BusinessId);

            // Car Pricing Rule
            modelBuilder.Entity<CarPricingRule>()
                .HasOne(p => p.Car)
                .WithMany(c => c.PricingRules)
                .HasForeignKey(p => p.CarId);

            modelBuilder.Entity<CarPricingRule>()
                .Property(p => p.Value)
                .HasPrecision(18, 2);

            // Car Images
            modelBuilder.Entity<CarImage>()
                .HasOne(ci => ci.Car)
                .WithMany(c => c.Images)
                .HasForeignKey(ci => ci.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            // Reservation
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.CarId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Client)
                .WithMany(c => c.Reservations)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Business)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BusinessId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ReservationStatus)
                .WithMany()
                .HasForeignKey(r => r.ReservationStatusId);

            modelBuilder.Entity<Reservation>()
                .Property(r => r.TotalPrice)
                .HasPrecision(18, 2);

            // Contract
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Reservation)
                .WithOne(r => r.Contract)
                .HasForeignKey<Contract>(c => c.ReservationId);

            // Payment
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.ReservationId);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            // Business Locations
            modelBuilder.Entity<BusinessLocation>()
                .HasOne(bl => bl.Business)
                .WithMany(b => b.Locations)
                .HasForeignKey(bl => bl.BusinessId);

            modelBuilder.Entity<BusinessLocation>()
                .HasOne(bl => bl.Location)
                .WithMany()
                .HasForeignKey(bl => bl.LocationId);

            // CarBrand -> CarModel
            modelBuilder.Entity<CarModel>()
                .HasOne(cm => cm.CarBrand)
                .WithMany(cb => cb.Models)
                .HasForeignKey(cm => cm.CarBrandId);

            modelBuilder.Entity<CarPricingRule>()
                .Property(p => p.PricePerDay)
                .HasPrecision(18, 2);
        }
    }
}
