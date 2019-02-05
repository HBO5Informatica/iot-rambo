namespace Group14.Rambo.Api.Data.Configuration
{
    using System;
    using Lib.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class NodeDeviceConfiguration : DeviceBaseConfiguration<NodeDevice>
    {
        public override void Configure(EntityTypeBuilder<NodeDevice> builder)
        {
            base.Configure(builder);

            builder.HasBaseType((Type)null);

            builder
                .HasMany(e => e.RoutedActors)
                .WithOne(s => s.Router)
                .IsRequired();

            builder
                .HasMany(e => e.RoutedSensors)
                .WithOne(s => s.Router)
                .IsRequired();
        }
    }
}
