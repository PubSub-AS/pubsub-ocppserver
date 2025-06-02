using Microsoft.EntityFrameworkCore;
using PubSub.OcppServer.Models.EF;

namespace PubSub.OcppServer.Data
{
    public class PgsqlContext : DbContext
    {
        public PgsqlContext(DbContextOptions<PgsqlContext> options) : base(options)
        {
        }

        public DbSet<ChargingPoint> ChargingPoints { get; set; }
        public DbSet<ChargingTransaction> ChargingTransactions { get; set; }
        public DbSet<ChargingPointProvider> ChargingPointProviders { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FacilityOwner> FacilityOwners { get; set; }
        public DbSet<IdTag> IdTags { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MeterValue> MeterValues { get; set; }
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChargingPoint>()?.ToTable("ChargingPoints");
            modelBuilder.Entity<ChargingTransaction>().ToTable("ChargingTransactions");
            modelBuilder.Entity<ChargingPointProvider>().ToTable("ChargingPointProviders");
            modelBuilder.Entity<Connector>().ToTable("Connectors");
            modelBuilder.Entity<Facility>().ToTable("Facilities");
            modelBuilder.Entity<IdTag>().ToTable("IdTags");
            modelBuilder.Entity<MeterValue>().ToTable("MeterValues");
            modelBuilder.Entity<Reservation>().ToTable("Reservations");
            modelBuilder.Entity<User>().ToTable("Users")
                .HasDiscriminator<string>("Discriminator")
                .HasValue<User>("User")
                .HasValue<ApiUser>("ApiUser")
                .HasValue<ChargePointUser>("ChargePointUser");
            modelBuilder.Entity<Vehicle>().ToTable("Vehicle");
        }
    }
}
