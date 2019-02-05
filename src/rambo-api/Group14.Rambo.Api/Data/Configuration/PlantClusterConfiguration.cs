namespace Group14.Rambo.Api.Data.Configuration
{
    using System;
    using Lib.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PlantClusterConfiguration : EntityBaseConfiguration<PlantCluster>
    {
        public override void Configure(EntityTypeBuilder<PlantCluster> builder)
        {
            base.Configure(builder);

            builder.HasBaseType((Type)null);

            builder.Property(e => e.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasOne(e => e.ActorUnit)
                .WithOne(a => a.Cluster)
                .HasForeignKey<PlantCluster>(e => e.ActorUnitId)
                .IsRequired(false);

            builder.HasOne(e => e.SensorUnit)
                .WithOne(s => s.Cluster)
                .HasForeignKey<PlantCluster>(e => e.SensorUnitId)
                .IsRequired(false);

            builder.HasOne(e => e.Family)
                .WithMany(f => f.Clusters)
                .HasForeignKey(e => e.FamilyId)
                .IsRequired(false);
        }
    }
}
