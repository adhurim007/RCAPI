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
  
        public DbSet<Business> Businesses { get; set; }
        public DbSet<CarImage> CarImages { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<CarPricingRule> CarPricingRules { get; set; }
        public DbSet<FuelType> FuelTypes { get; set; }
        public DbSet<Transmission> Transmissions { get; set; }
        public DbSet<Car> Cars { get; set; } 
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<ReservationStatusHistory> ReservationStatusHistories { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<ExtraService> ExtraServices { get; set; }
        public DbSet<ReservationExtraService> ReservationExtraServices { get; set; }

        // NEW — Business Location (single table)
        public DbSet<BusinessLocations> BusinessLocations { get; set; }
        public DbSet<Customer> Customer { get; set; }

        // Other modules
        public DbSet<Menu> Menus { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Translation> Translation { get; set; }

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<VehicleInspection> VehicleInspection { get; set; }
        public DbSet<VehicleInspectionPhoto> VehicleInspectionPhoto { get; set; } 
        public DbSet<VehicleDamage> VehicleDamage { get; set; }
        public DbSet<VehicleDamagePhoto> VehicleDamagePhoto { get; set; }

        public DbSet<CarRegistration> CarRegistrations { get; set; }
        public DbSet<CarService> CarServices { get; set; }

        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CarRegistration>()
             .HasOne(r => r.Car)
             .WithMany(c => c.Registrations)
             .HasForeignKey(r => r.CarId)
             .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarService>()
                .HasOne(s => s.Car)
                .WithMany(c => c.Services)
                .HasForeignKey(s => s.CarId)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<CarService>()
                    .HasOne(cs => cs.Business)
                    .WithMany()
                    .HasForeignKey(cs => cs.BusinessId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CarRegistration>()
                .HasOne(cr => cr.Business)
                .WithMany()
                .HasForeignKey(cr => cr.BusinessId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Customer>()
                .HasOne(c => c.User)
                .WithOne(u => u.Client)
                .HasForeignKey<Customer>(c => c.UserId);

             
            modelBuilder.Entity<Business>()
                .HasOne(b => b.User)
                .WithOne(u => u.Business)
                .HasForeignKey<Business>(b => b.UserId);

         
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

           
            modelBuilder.Entity<BusinessLocations>()
                .HasOne(bl => bl.Business)
                .WithMany(b => b.Locations)
                .HasForeignKey(bl => bl.BusinessId)
                .OnDelete(DeleteBehavior.Cascade);

          
            modelBuilder.Entity<BusinessLocations>()
                .HasOne(bl => bl.State)
                .WithMany()
                .HasForeignKey(bl => bl.StateId)
                .OnDelete(DeleteBehavior.Restrict);

          
            modelBuilder.Entity<BusinessLocations>()
                .HasOne(bl => bl.City)
                .WithMany()
                .HasForeignKey(bl => bl.CityId)
                .OnDelete(DeleteBehavior.Restrict);

 
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

            modelBuilder.Entity<VehicleInspection>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Reservation)
                    .WithMany(r => r.Inspections)
                    .HasForeignKey(x => x.ReservationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Business)
                    .WithMany()
                    .HasForeignKey(x => x.BusinessId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(x => x.Photos)
                    .WithOne(p => p.Inspection)
                    .HasForeignKey(p => p.InspectionId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<VehicleDamage>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Reservation)
                    .WithMany(r => r.Damages)
                    .HasForeignKey(x => x.ReservationId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(x => x.Business)
                    .WithMany()
                    .HasForeignKey(x => x.BusinessId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.EstimatedCost)
                    .HasPrecision(18, 2);

                entity.HasMany(x => x.Photos)
                    .WithOne(p => p.Damage)
                    .HasForeignKey(p => p.DamageId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CarPricingRule>()
            .HasOne(r => r.Car)
            .WithMany(c => c.PricingRules)
            .HasForeignKey(r => r.CarId)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<CarPricingRule>()
                .Property(r => r.PricePerDay)
                .HasPrecision(18, 2);

            
            modelBuilder.Entity<CarImage>()
                .HasOne(ci => ci.Car)
                .WithMany(c => c.Images)
                .HasForeignKey(ci => ci.CarId)
                .OnDelete(DeleteBehavior.Cascade);

          
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

           
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Reservation)
                .WithOne(r => r.Contract)
                .HasForeignKey<Contract>(c => c.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

           
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithMany(r => r.Payments)
                .HasForeignKey(p => p.ReservationId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.Business)
                .WithMany(b => b.Reservations)
                .HasForeignKey(r => r.BusinessId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.ReservationStatus)
                .WithMany()
                .HasForeignKey(r => r.ReservationStatusId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReservationExtraService>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.HasOne(x => x.Reservation)
                      .WithMany(r => r.ExtraServices)
                      .HasForeignKey(x => x.ReservationId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.ExtraService)
                      .WithMany(e => e.ReservationExtraServices)
                      .HasForeignKey(x => x.ExtraServiceId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.Property(x => x.PricePerDay)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();

                entity.Property(x => x.TotalPrice)
                      .HasColumnType("decimal(18,2)")
                      .IsRequired();
            });
             
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.Property(r => r.TotalPriceWithoutDiscount)
                      .HasColumnType("decimal(18,2)");

                entity.Property(r => r.TotalPrice)
                      .HasColumnType("decimal(18,2)");

                entity.Property(r => r.Discount)
                      .HasColumnType("decimal(18,2)");
            });
             
            modelBuilder.Entity<Translation>()
                .HasIndex(t => new { t.LanguageId, t.Key })
                .IsUnique();
        }
    }

}
