namespace Group14.Rambo.Api.Data.Configuration
{
    using System;
    using Lib.Entities;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class DataTestConfiguration : EntityBaseConfiguration<DataTest>
    {
        public override void Configure(EntityTypeBuilder<DataTest> builder)
        {
            base.Configure(builder);

            builder.HasBaseType((Type)null);
        }
    }
}
