namespace Group14.Rambo.Lib.Entities
{
    using System.Collections.Generic;
    /// <inheritdoc />
    /// <summary>
    /// Represents a plant type with its optimal conditions
    /// </summary>
    public class PlantFamily : EntityBase
    {
        /// <summary>
        /// Gets or sets the friendly name of this plant family
        /// </summary>
        /// <remarks>
        /// Doesn't have to be a real plant family. This can be a name to group plants with similar optimal conditions
        /// </remarks>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the optimal humidity of the soil, expressed in percent
        /// </summary>
        public float SoilMoisture { get; set; }

        /// <summary>
        /// Gets or sets the optimal light intensity during the day, expressed in lumen
        /// </summary>
        public float LightIntensity { get; set; }

        /// <summary>
        /// Gets or sets the optimal air humidity, expressed in percent
        /// </summary>
        public float AirHumidity { get; set; }

        /// <summary>
        /// Gets or sets the optimal air humidity, expressed in Celsius
        /// </summary>
        public float AirTemperature { get; set; }

        /// <summary>
        /// Gets or sets a collection of <see cref="PlantCluster"/> entities belonging to this family
        /// </summary>
        public ICollection<PlantCluster> Clusters { get; set; }
    }
}
