namespace Group14.Rambo.Api.Data.Configuration
{
    using Lib.Entities;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
    using System;

    public abstract class EntityBaseConfiguration<T> : IEntityTypeConfiguration<T>
        where T : EntityBase
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasBaseType((Type)null);

            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).UseSqlServerIdentityColumn();
        }
    }
}
