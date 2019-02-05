using Group14.Rambo.Lib.Entities;

namespace Group14.Rambo.Lib.Dto
{
    /// <summary>
    /// DTO object, routed through a <see cref="NodeDevice"/> containing a <see cref="SensorData"/> as destination
    /// </summary>
    public class SensorData : DtoBase
    {
        /// <summary>
        /// HW address of the originating <see cref="SensorDevice"/>
        /// </summary>
        public string SensorAddress { get; set; }

        /// <summary>
        /// HW address of the <see cref="NodeDevice"/> routing this message
        /// </summary>
        public string NodeAddress { get; set; }

        /// <summary>
        /// device-unique overflowing message ID
        /// </summary>
        public ushort MessageId { get; set; }

        /// <summary>
        /// Reported capabilities of the originating <see cref="SensorDevice"/>
        /// </summary>
        public SensorCapabilities Capabilities { get; set; }

        /// <summary>
        /// Moisture of the soil reported by the <see cref="SensorDevice"/> 
        /// </summary>
        public ushort SoilMoisture { get; set; }

        /// <summary>
        /// Air Humidity reported by the <see cref="SensorDevice"/> 
        /// </summary>
        public float Humidity { get; set; }

        /// <summary>
        /// Air Temperature reported by the <see cref="SensorDevice"/> 
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// Light level reported by the <see cref="SensorDevice"/>
        /// </summary>
        public float LightLevel { get; set; }

    }
}
