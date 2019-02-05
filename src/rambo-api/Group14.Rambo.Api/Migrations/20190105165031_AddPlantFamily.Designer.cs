﻿// <auto-generated />
using System;
using Group14.Rambo.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Group14.Rambo.Api.Migrations
{
    [DbContext(typeof(RamboContext))]
    [Migration("20190105165031_AddPlantFamily")]
    partial class AddPlantFamily
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.ActorDevice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Capabilities");

                    b.Property<string>("HardwareAddress")
                        .IsRequired()
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<long>("RouterId");

                    b.HasKey("Id");

                    b.HasIndex("RouterId");

                    b.ToTable("ActorDevice");

                    b.HasData(
                        new { Id = 1L, Capabilities = (byte)7, HardwareAddress = "4673EFC", Name = "Office group 1 Actor", RouterId = 1L },
                        new { Id = 2L, Capabilities = (byte)7, HardwareAddress = "8763EFC", Name = "Office group 2 Actor", RouterId = 1L },
                        new { Id = 3L, Capabilities = (byte)7, HardwareAddress = "9872AED", Name = "Reception flowerbed 1 Actor", RouterId = 1L }
                    );
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.AnalyticStep", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateTimeChecked");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("AnalyticSteps");
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.DataTest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApiData");

                    b.HasKey("Id");

                    b.ToTable("DataTest");
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.NodeDevice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("HardwareAddress")
                        .IsRequired()
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("NodeDevice");

                    b.HasData(
                        new { Id = 1L, HardwareAddress = "030405060708", Name = "Rambo Node" }
                    );
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.PlantCluster", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<long?>("ActorUnitId");

                    b.Property<long?>("FamilyId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<int>("NumberOfPlants");

                    b.Property<long?>("SensorUnitId");

                    b.HasKey("Id");

                    b.HasIndex("ActorUnitId")
                        .IsUnique()
                        .HasFilter("[ActorUnitId] IS NOT NULL");

                    b.HasIndex("FamilyId");

                    b.HasIndex("SensorUnitId")
                        .IsUnique()
                        .HasFilter("[SensorUnitId] IS NOT NULL");

                    b.ToTable("PlantCluster");

                    b.HasData(
                        new { Id = 1L, ActorUnitId = 1L, FamilyId = 1L, Name = "Office group 1", NumberOfPlants = 2, SensorUnitId = 1L },
                        new { Id = 2L, ActorUnitId = 2L, FamilyId = 2L, Name = "Office group 2", NumberOfPlants = 1, SensorUnitId = 2L },
                        new { Id = 3L, ActorUnitId = 3L, FamilyId = 1L, Name = "Reception flowerbed 1", NumberOfPlants = 6, SensorUnitId = 3L }
                    );
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.PlantFamily", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("AirHumidity");

                    b.Property<float>("AirTemperature");

                    b.Property<float>("LightIntensity");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<float>("SoilMoisture");

                    b.HasKey("Id");

                    b.ToTable("PlantFamilies");

                    b.HasData(
                        new { Id = 1L, AirHumidity = 0.55f, AirTemperature = 20f, LightIntensity = 250f, Name = "Heat intolerant - Shade", SoilMoisture = 0.3f },
                        new { Id = 2L, AirHumidity = 0.7f, AirTemperature = 25f, LightIntensity = 40000f, Name = "Thirsty - Sunlight", SoilMoisture = 0.5f }
                    );
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.SensorDevice", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Capabilities");

                    b.Property<string>("HardwareAddress")
                        .IsRequired()
                        .HasMaxLength(36)
                        .IsUnicode(false);

                    b.Property<string>("Name")
                        .HasMaxLength(100);

                    b.Property<long>("RouterId");

                    b.HasKey("Id");

                    b.HasIndex("RouterId");

                    b.ToTable("SensorDevices");

                    b.HasData(
                        new { Id = 1L, Capabilities = (byte)15, HardwareAddress = "4673EFC", Name = "Office group 1 Sensors", RouterId = 1L },
                        new { Id = 2L, Capabilities = (byte)15, HardwareAddress = "8763EFC", Name = "Office group 2 Sensors", RouterId = 1L },
                        new { Id = 3L, Capabilities = (byte)15, HardwareAddress = "9872AED", Name = "Reception flowerbed 1 Sensors", RouterId = 1L }
                    );
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.SensorRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<float>("Humidity");

                    b.Property<float>("LightLevel");

                    b.Property<int>("MessageId");

                    b.Property<DateTime>("RegisteredDateTime");

                    b.Property<long?>("SensorDeviceId");

                    b.Property<int>("SoilMoisture");

                    b.Property<float>("Temperature");

                    b.HasKey("Id");

                    b.HasIndex("SensorDeviceId");

                    b.ToTable("SensorRecords");
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.ActorDevice", b =>
                {
                    b.HasOne("Group14.Rambo.Lib.Entities.NodeDevice", "Router")
                        .WithMany("RoutedActors")
                        .HasForeignKey("RouterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.PlantCluster", b =>
                {
                    b.HasOne("Group14.Rambo.Lib.Entities.ActorDevice", "ActorUnit")
                        .WithOne("Cluster")
                        .HasForeignKey("Group14.Rambo.Lib.Entities.PlantCluster", "ActorUnitId");

                    b.HasOne("Group14.Rambo.Lib.Entities.PlantFamily", "Family")
                        .WithMany("Clusters")
                        .HasForeignKey("FamilyId");

                    b.HasOne("Group14.Rambo.Lib.Entities.SensorDevice", "SensorUnit")
                        .WithOne("Cluster")
                        .HasForeignKey("Group14.Rambo.Lib.Entities.PlantCluster", "SensorUnitId");
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.SensorDevice", b =>
                {
                    b.HasOne("Group14.Rambo.Lib.Entities.NodeDevice", "Router")
                        .WithMany("RoutedSensors")
                        .HasForeignKey("RouterId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Group14.Rambo.Lib.Entities.SensorRecord", b =>
                {
                    b.HasOne("Group14.Rambo.Lib.Entities.SensorDevice", "SensorDevice")
                        .WithMany()
                        .HasForeignKey("SensorDeviceId");
                });
#pragma warning restore 612, 618
        }
    }
}
