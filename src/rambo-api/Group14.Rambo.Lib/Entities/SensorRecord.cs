using System;
using System.Collections.Generic;
using System.Text;

namespace Group14.Rambo.Lib.Entities
{
    public class SensorRecord : EntityBase
    {
        public SensorDevice SensorDevice { get; set; }
        public ushort MessageId { get; set; }
        public ushort SoilMoisture { get; set; }
        public float Humidity { get; set; }
        public float Temperature { get; set; }
        public float LightLevel { get; set; }
        public DateTime RegisteredDateTime { get; set; }

    }
}
