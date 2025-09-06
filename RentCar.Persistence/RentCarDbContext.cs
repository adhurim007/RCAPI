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
        public DbSet<Menu> Menus { get; set; } 
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Language> Language { get; set; }
        public DbSet<Translation> Translation { get; set; }

        public DbSet<ReservationStatusHistory> ReservationStatusHistories { get; set; }

        public DbSet<State> States { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); 
            //modelBuilder.Entity<Car>().HasQueryFilter(c =>
            //  !_tenantProvider.IsSuperAdmin() && _tenantProvider.GetBusinessId().HasValue
            //      ? c.BusinessId == _tenantProvider.GetBusinessId()
            //      : true);

            modelBuilder.Entity<Client>()
                .HasOne(c => c.User)
                .WithOne(u => u.Client)
                .HasForeignKey<Client>(c => c.UserId);

            modelBuilder.Entity<Business>()
                .HasOne(b => b.User)
                .WithOne(u => u.Business)
                .HasForeignKey<Business>(b => b.UserId);

            modelBuilder.Entity<CarPricingRule>()
               .Property(c => c.DaysOfWeek)
               .HasPrecision(18, 2);
             
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

            
            modelBuilder.Entity<CarPricingRule>()
                .HasOne(p => p.Car)
                .WithMany(c => c.PricingRules)
                .HasForeignKey(p => p.CarId);

            modelBuilder.Entity<CarPricingRule>()
                .Property(p => p.Value)
                .HasPrecision(18, 2);

            
            modelBuilder.Entity<CarImage>()
                .HasOne(ci => ci.Car)
                .WithMany(c => c.Images)
                .HasForeignKey(ci => ci.CarId)
                .OnDelete(DeleteBehavior.Cascade);

             
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
             
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Reservation)
                .WithOne(r => r.Contract)
                .HasForeignKey<Contract>(c => c.ReservationId);
       
            modelBuilder.Entity<Payment>()
                .HasOne(p => p.Reservation)
                .WithOne(r => r.Payment)
                .HasForeignKey<Payment>(p => p.ReservationId);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);
             
            modelBuilder.Entity<BusinessLocation>()
                .HasOne(bl => bl.Business)
                .WithMany(b => b.Locations)
                .HasForeignKey(bl => bl.BusinessId);

            modelBuilder.Entity<BusinessLocation>()
                .HasOne(bl => bl.Location)
                .WithMany()
                .HasForeignKey(bl => bl.LocationId);

            
            modelBuilder.Entity<CarModel>()
                .HasOne(cm => cm.CarBrand)
                .WithMany(cb => cb.Models)
                .HasForeignKey(cm => cm.CarBrandId);

            modelBuilder.Entity<CarPricingRule>()
                .Property(p => p.PricePerDay)
                .HasPrecision(18, 2);

            modelBuilder.Entity<ReservationStatusHistory>()
                .HasOne(h => h.Reservation)
                .WithMany(r => r.StatusHistories)
                .HasForeignKey(h => h.ReservationId)
                .OnDelete(DeleteBehavior.Restrict);   
             
            modelBuilder.Entity<ReservationStatusHistory>()
                .HasOne(h => h.ReservationStatus)
                .WithMany()
                .HasForeignKey(h => h.ReservationStatusId);

            modelBuilder.Entity<Menu>()
               .HasMany(m => m.Children)
               .WithOne(m => m.Parent)
               .HasForeignKey(m => m.ParentId)
               .OnDelete(DeleteBehavior.Restrict);
             
            modelBuilder.Entity<Language>()
                .HasMany(l => l.Translations)
                .WithOne(t => t.Language)
                .HasForeignKey(t => t.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);

            // Keys should be unique per language
            modelBuilder.Entity<Translation>()
                .HasIndex(t => new { t.LanguageId, t.Key })
                .IsUnique();
             

            // Seed default language
            modelBuilder.Entity<Language>().HasData(
                new Language { Id = 1, Code = "en", Name = "English", IsActive = true },
                new Language { Id = 2, Code = "sq", Name = "Albanian", IsActive = true }
            );

            // State -> Cities
            modelBuilder.Entity<State>()
                .HasMany(s => s.Cities)
                .WithOne(c => c.State)
                .HasForeignKey(c => c.StateId)
                .OnDelete(DeleteBehavior.Cascade);

            // Business -> State
            modelBuilder.Entity<Business>()
                .HasOne(b => b.State)
                .WithMany()
                .HasForeignKey(b => b.StateId)
                .OnDelete(DeleteBehavior.Restrict); // 👈 prevent cascade

            // Business -> City
            modelBuilder.Entity<Business>()
                .HasOne(b => b.City)
                .WithMany()
                .HasForeignKey(b => b.CityId)
                .OnDelete(DeleteBehavior.Restrict); // 👈 prevent cascade

            modelBuilder.Entity<Business>()
            .Property(c => c.Address);

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Id).HasColumnOrder(0);
                entity.Property(e => e.ParentId).HasColumnOrder(1);
                entity.Property(e => e.Title).HasColumnOrder(2);
                entity.Property(e => e.Subtitle).HasColumnOrder(3);
                entity.Property(e => e.Type).HasColumnOrder(4);
                entity.Property(e => e.Icon).HasColumnOrder(5);
                entity.Property(e => e.Link).HasColumnOrder(6);
                entity.Property(e => e.HasSubMenu).HasColumnOrder(7);
                entity.Property(e => e.Claim).HasColumnName("Claim").HasColumnOrder(8);
                entity.Property(e => e.Active).HasColumnOrder(9);
                entity.Property(e => e.SortNumber).HasColumnOrder(10);
                entity.Property(e => e.CreatedBy).HasColumnOrder(11);
                entity.Property(e => e.CreatedOn).HasColumnOrder(12);
                entity.Property(e => e.LastModifiedBy).HasColumnOrder(13);
                entity.Property(e => e.LastModifiedOn).HasColumnOrder(14);
                entity.Property(e => e.DeletedBy).HasColumnOrder(15);
                entity.Property(e => e.DeletedOn).HasColumnOrder(16);
            });

        }
    }
}
