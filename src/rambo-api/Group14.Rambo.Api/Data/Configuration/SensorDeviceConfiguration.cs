namespace Group14.Rambo.Api.Data.Configuration
{
    using System;
    using Lib.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SensorDeviceConfiguration : DeviceBaseConfiguration<SensorDevice>
    {
        public override void Configure(EntityTypeBuilder<SensorDevice> builder)
        {
            base.Configure(builder);

            builder.HasBaseType((Type)null);

            builder.HasOne(e => e.Router)
                .WithMany(r => r.RoutedSensors)
                .HasForeignKey(e => e.RouterId)
                .IsRequired();
        }
    }
}
