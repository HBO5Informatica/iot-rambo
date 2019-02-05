namespace Group14.Rambo.Lib.Entities
{
    using System.Collections.Generic;

    /// <inheritdoc />
    /// <summary>
    /// Represents a routing node between the API and the <see cref="T:Group14.Rambo.Lib.Entities.SensorDevice" /> or <see cref="T:Group14.Rambo.Lib.Entities.ActorDevice" /> components
    /// </summary>
    public class NodeDevice : DeviceBase
    {
        /// <summary>
        /// Gets or sets a collection of <see cref="ActorDevice"/> this <see cref="NodeDevice"/> can route
        /// </summary>
        public ICollection<ActorDevice> RoutedActors { get; set; }

        /// <summary>
        /// Gets or sets a collection of <see cref="ActorDevice"/> this <see cref="NodeDevice"/> can route
        /// </summary>
        public ICollection<SensorDevice> RoutedSensors { get; set; }
    }
}
