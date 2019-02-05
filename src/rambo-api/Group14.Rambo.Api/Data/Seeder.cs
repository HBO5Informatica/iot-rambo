using Group14.Rambo.Lib.Entities;
using Microsoft.EntityFrameworkCore;

namespace Group14.Rambo.Api.Data
{
    public class Seeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<NodeDevice>().HasData(
                new NodeDevice {
                    Id = 1, Name = "Rambo Node",
                    HardwareAddress = "030405060708"
                }
            );

            modelBuilder.Entity<ActorDevice>().HasData(
                new ActorDevice {
                    Id = 1, Name = "Office group 1 Actor",
                    HardwareAddress = "4673EFC",
                    RouterId = 1,
                    Capabilities = ActorCapabilities.AddWater | ActorCapabilities.AdjustHeat | ActorCapabilities.AdjustLight,
                },
                new ActorDevice {
                    Id = 2, Name = "Office group 2 Actor",
                    HardwareAddress = "8763EFC",
                    RouterId = 1,
                    Capabilities = ActorCapabilities.AddWater | ActorCapabilities.AdjustHeat | ActorCapabilities.AdjustLight,
                },
                new ActorDevice {
                    Id = 3, Name = "Reception flowerbed 1 Actor",
                    HardwareAddress = "9872AED",
                    RouterId = 1,
                    Capabilities = ActorCapabilities.AddWater | ActorCapabilities.AdjustHeat | ActorCapabilities.AdjustLight,
                }
            );
            
            modelBuilder.Entity<SensorDevice>().HasData(
                new SensorDevice {
                    Id = 1, Name = "Office group 1 Sensors",
                    HardwareAddress = "4673EFC",
                    RouterId = 1,
                    Capabilities = SensorCapabilities.Thermometer | SensorCapabilities.MoistureMeter | SensorCapabilities.LuxMeter | SensorCapabilities.Hygrometer,
                },
                new SensorDevice {
                    Id = 2, Name = "Office group 2 Sensors",
                    HardwareAddress = "8763EFC",
                    RouterId = 1,
                    Capabilities = SensorCapabilities.Thermometer | SensorCapabilities.MoistureMeter | SensorCapabilities.LuxMeter | SensorCapabilities.Hygrometer,
                },
                new SensorDevice {
                    Id = 3, Name = "Reception flowerbed 1 Sensors",
                    HardwareAddress = "9872AED",
                    RouterId = 1,
                    Capabilities = SensorCapabilities.Thermometer | SensorCapabilities.MoistureMeter | SensorCapabilities.LuxMeter | SensorCapabilities.Hygrometer,
                }
            );

            modelBuilder.Entity<PlantFamily>().HasData(
                new PlantFamily {   Id = 1, Name = "Heat intolerant - Shade",
                                    SoilMoisture = 0.30f, AirHumidity = 0.55f,
                                    AirTemperature = 20f, LightIntensity = 250 },
                new PlantFamily {   Id = 2, Name = "Thirsty - Sunlight",
                                    SoilMoisture = 0.50f, AirHumidity = 0.70f,
                                    AirTemperature = 25f, LightIntensity = 40000 }
            );

            modelBuilder.Entity<PlantCluster>().HasData(
                new PlantCluster
                {
                    Id = 1, FamilyId = 1,
                    Name = "Office group 1",
                    NumberOfPlants = 2,
                    ActorUnitId = 1,
                    SensorUnitId = 1,
                },
                new PlantCluster
                {
                    Id = 2, FamilyId = 2,
                    Name = "Office group 2",
                    NumberOfPlants = 1,
                    ActorUnitId = 2,
                    SensorUnitId = 2,
                },
                new PlantCluster
                {
                    Id = 3, FamilyId = 1,
                    Name = "Reception flowerbed 1",
                    NumberOfPlants = 6,
                    ActorUnitId = 3,
                    SensorUnitId = 3,
                }
            );

        }
    }
}
