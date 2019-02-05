namespace Group14.Rambo.Api.Data.Configuration
{
    using System;
    using Lib.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public abstract class DeviceBaseConfiguration<T> : EntityBaseConfiguration<T> where T : DeviceBase
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);

            builder.HasBaseType((Type)null);
            
            builder.Property(e => e.HardwareAddress)
                .IsRequired()
                .HasMaxLength(36)
                .IsUnicode(false);

            builder.Property(e => e.Name)
                .HasMaxLength(100);
        }
    }
}
