using Microsoft.EntityFrameworkCore;
using DotnetFinalAssessment.Models;

namespace DotnetFinalAssessment.Data
{
    public class dbContext: DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Claim> Claims { get; set; }

        public dbContext(DbContextOptions<dbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(owner =>
            {
                owner.ToTable("owners");
                owner.HasKey(p => p.Id);
                owner.Property(p => p.FirstName).IsRequired().HasMaxLength(150);
                owner.Property(p => p.LastName).IsRequired();
                owner.Property(p => p.DriverLicense).IsRequired();

            });

            modelBuilder.Entity<Vehicle>(vehicle =>
            {
                vehicle.ToTable("vehicles");
                vehicle.HasKey(p => p.Id);
                vehicle.
                HasOne(p => p.Owner).
                WithMany(p => p.Vehicles).
                HasForeignKey(p => p.OwnerId);
                vehicle.Property(p => p.Brand).IsRequired().HasMaxLength(60);
                vehicle.Property(p => p.Vin).IsRequired();
                vehicle.Property(p => p.Color).IsRequired();
                vehicle.Property(p => p.Date).IsRequired();
                vehicle.Property(p => p.OwnerId).IsRequired();
            });

            modelBuilder.Entity<Claim>(claim =>
            {
                claim.ToTable("claims");
                claim.HasKey(p => p.Id);
                claim.
                HasOne(p => p.Vehicle).
                WithMany(p => p.Claims).
                HasForeignKey(p => p.VehicleId);
                claim.Property(p => p.Description).IsRequired().HasMaxLength(60);
                claim.Property(p => p.Status).IsRequired();
                claim.Property(p => p.Date).IsRequired();
                claim.Property(p => p.VehicleId).IsRequired();
            });
        }
    }
}
