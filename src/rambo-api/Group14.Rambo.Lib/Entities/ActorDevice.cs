namespace Group14.Rambo.Lib.Entities
{
    /// <summary>
    /// Represents a device containing environment controllers, typically bound to a <see cref="PlantCluster"/>
    /// </summary>
    public class ActorDevice : DeviceBase
    {
        /// <summary>
        /// Contains the capabilities of this <see cref="ActorDevice"/>
        /// </summary>
        public ActorCapabilities Capabilities { get; set; }

        /// <summary>
        /// The <see cref="PlantCluster"/> whose environment is controlled by this <see cref="ActorDevice"/>
        /// </summary>
        public PlantCluster Cluster { get; set; }

        /// <summary>
        ///  Gets or sets the foreign key property of the related <see cref="NodeDevice"/>
        /// </summary>
        public long RouterId { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="NodeDevice"/> which routes this <see cref="ActorDevice"/>
        /// </summary>
        public NodeDevice Router { get; set; }
    }
}
