using Group14.Rambo.Lib.Entities;

namespace Group14.Rambo.Api.Data
{
    using Configuration;
    using Microsoft.EntityFrameworkCore;

    public class RamboContext : DbContext
    {
        public RamboContext(DbContextOptions<RamboContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PlantFamilyConfiguration());
            modelBuilder.ApplyConfiguration(new PlantClusterConfiguration());
            modelBuilder.ApplyConfiguration(new NodeDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new SensorDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new ActorDeviceConfiguration());
            modelBuilder.ApplyConfiguration(new DataTestConfiguration());

            Seeder.Seed(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<SensorDevice> SensorDevices { get; set; }
        public DbSet<SensorRecord> SensorRecords { get; set; }
        public DbSet<AnalyticStep> AnalyticSteps { get; set; }
        public DbSet<PlantFamily> PlantFamilies { get; set; }
        
    }
}
