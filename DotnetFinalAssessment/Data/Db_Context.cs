using Microsoft.EntityFrameworkCore;
using DotnetFinalAssessment.Models;

namespace DotnetFinalAssessment.Data
{
    public class Db_Context: DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Claim> Claims { get; set; }

        public Db_Context(DbContextOptions<Db_Context> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Owner>(owner =>
            {
                owner.ToTable("owners");
                owner.HasKey(p => p.Id);
                owner.Property(p => p.FirstName).IsRequired().HasMaxLength(45);
                owner.Property(p => p.LastName).IsRequired().HasMaxLength(45);
                owner.Property(p => p.DriverLicense).IsRequired().HasMaxLength(45);

            });

            modelBuilder.Entity<Vehicle>(vehicle =>
            {
                vehicle.ToTable("vehicles");
                vehicle.HasKey(p => p.Id);
                vehicle.
                HasOne(p => p.Owner).
                WithMany(p => p.Vehicles).
                HasForeignKey(p => p.OwnerId);
                vehicle.Property(p => p.Brand).IsRequired().HasMaxLength(45);
                vehicle.Property(p => p.Vin).IsRequired().HasMaxLength(45);
                vehicle.Property(p => p.Color).IsRequired().HasMaxLength(45);
                vehicle.Property(p => p.Year).IsRequired();
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
                claim.Property(p => p.Description).IsRequired().HasMaxLength(45);
                claim.Property(p => p.Status).IsRequired().HasMaxLength(45);
                claim.Property(p => p.Date).IsRequired();
                claim.Property(p => p.VehicleId).IsRequired();
            });
        }
    }
}
