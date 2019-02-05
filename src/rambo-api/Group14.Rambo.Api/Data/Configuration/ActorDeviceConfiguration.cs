namespace Group14.Rambo.Api.Data.Configuration
{
    using System;
    using Lib.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class ActorDeviceConfiguration : DeviceBaseConfiguration<ActorDevice>
    {
        public override void Configure(EntityTypeBuilder<ActorDevice> builder)
        {
            base.Configure(builder);

            builder.HasBaseType((Type)null);

            builder.HasOne(e => e.Router)
                .WithMany(r => r.RoutedActors)
                .HasForeignKey(e => e.RouterId)
                .IsRequired();
        }
    }
}
