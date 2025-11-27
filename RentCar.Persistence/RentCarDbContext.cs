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

        // ===========================
        // ENTITY SETS
        // ===========================

        
        public DbSet<Business> Businesses { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<CarPricingRule> CarPricingRules { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Car> Cars { get; set; }

        // Reservations
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatusHistory> ReservationStatusHistories { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ExtraService> ExtraServices { get; set; }
        public DbSet<ReservationExtraService> ReservationExtraServices { get; set; }

        // NEW — Business Location (single table)
        public DbSet<BusinessLocations> BusinessLocations { get; set; }

        // Other modules
        public DbSet<Menu> Menus { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Translation> Translation { get; set; }

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ===========================
            // CLIENT → USER
            // ===========================
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Client)
                .HasForeignKey<Customer>(c => c.UserId);

            // ===========================
            // BUSINESS → USER
            // ===========================
            modelBuilder.Entity<Business>()
                .HasOne(b => b.User)
                .WithOne(u => u.Business)
                .HasForeignKey<Business>(b => b.UserId);

            // ===========================
            // BUSINESS → STATE / CITY
            // ===========================
            modelBuilder.Entity<Business>()
                .HasOne(b => b.State)
                .WithMany()
                .HasForeignKey(b => b.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Business>()
                .HasOne(b => b.City)
                .WithMany()
                .HasForeignKey(b => b.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===========================
            // BUSINESS → BUSINESS LOCATIONS
            // ===========================
            // BUSINESS → LOCATIONS
            modelBuilder.Entity<BusinessLocations>()
                .HasOne(bl => bl.Business)
                .WithMany(b => b.Locations)
                .HasForeignKey(bl => bl.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

            // STATE
            modelBuilder.Entity<BusinessLocations>()
                .HasOne(bl => bl.State)
                .WithMany()
                .HasForeignKey(bl => bl.StateId)
                .OnDelete(DeleteBehavior.Restrict);

            // CITY
            modelBuilder.Entity<BusinessLocations>()
                .HasOne(bl => bl.City)
                .WithMany()
                .HasForeignKey(bl => bl.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===========================
            // CAR RELATIONS
            // ===========================
            modelBuilder.Entity<Car>()
                .Property(c => c.DailyPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarBrand)
                .WithMany()  
                .HasForeignKey(c => c.CarBrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarModel)
                .WithMany()
                .HasForeignKey(c => c.CarModelId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.CarType)
                .WithMany()
                .HasForeignKey(c => c.CarTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.FuelType)
                .WithMany()
                .HasForeignKey(c => c.FuelTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Transmission)
                .WithMany()
                .HasForeignKey(c => c.TransmissionId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Business)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BusinessId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CarPricingRule>()
            .HasOne(r => r.Car)
            .WithMany(c => c.PricingRules)
            .HasForeignKey(r => r.CarId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarPricingRule>()
                .Property(r => r.PricePerDay)
                .HasPrecision(18, 2);

            // ===========================
            // CAR IMAGES
            // ===========================
            modelBuilder.Entity<CarImage>()
                .HasOne(ci => ci.Car)
                .WithMany(c => c.Images)
                .HasForeignKey(ci => ci.CarId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===========================
            // RESERVATION → BUSINESS LOCATIONS
            // ===========================
            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.PickupLocation)
                .WithMany()
                .HasForeignKey(r => r.PickupLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.DropoffLocation)
                .WithMany()
                .HasForeignKey(r => r.DropoffLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // ===========================
            // CONTRACT
            // ===========================
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Reservation)
                .WithOne(r => r.Contract)
                .HasForeignKey<Contract>(c => c.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            // ===========================
            // PAYMENT
            // ===========================
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            // ===========================
            // RESERVATION EXTRA SERVICES
            // ===========================
            modelBuilder.Entity<ReservationExtraService>(entity =>
            {
                entity.HasKey(res => new { res.ReservationId, res.ExtraServiceId });

                entity.Property(res => res.Price)
                    .HasPrecision(18, 2);

                entity.HasOne(res => res.Reservation)
                    .WithMany(r => r.ExtraServices)
                    .HasForeignKey(res => res.ReservationId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(res => res.ExtraService)
                    .WithMany(es => es.ReservationExtraServices)
                    .HasForeignKey(res => res.ExtraServiceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // ===========================
            // TRANSLATIONS
            // ===========================
            modelBuilder.Entity<Translation>()
                .HasIndex(t => new { t.LanguageId, t.Key })
                .IsUnique();
        }
    }

}
