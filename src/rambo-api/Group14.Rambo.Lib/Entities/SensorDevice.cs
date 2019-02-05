namespace Group14.Rambo.Lib.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// Represents a device containing sensors, typically bound to a <see cref="T:Group14.Rambo.Lib.Entities.PlantCluster" />
    /// </summary>
    public class SensorDevice : DeviceBase
    {
        /// <summary>
        /// Gets or sets the capabilities of this <see cref="SensorDevice"/>
        /// </summary>
        public SensorCapabilities Capabilities { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="PlantCluster"/> whose environment is measured by this <see cref="SensorDevice"/>
        /// </summary>
        public PlantCluster Cluster { get; set; }

        /// <summary>
        ///  Gets or sets the foreign key property of the related <see cref="NodeDevice"/>
        /// </summary>
        public long RouterId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="NodeDevice"/> which routes this <see cref="SensorDevice"/>
        /// </summary>
        public NodeDevice Router { get; set; }
    }
}
