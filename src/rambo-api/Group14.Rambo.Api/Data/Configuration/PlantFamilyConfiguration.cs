namespace Group14.Rambo.Api.Data.Configuration
{
    using System;
    using Lib.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlantFamilyConfiguration : EntityBaseConfiguration<PlantFamily>
    {
        public override void Configure(EntityTypeBuilder<PlantFamily> builder)
        {
            base.Configure(builder);

            builder.HasBaseType((Type)null);

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();
            
        }
    }
}
