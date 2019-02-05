namespace Group14.Rambo.Lib.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// Defines a cluster of plants with added actors and sensors
    /// </summary>
    public class PlantCluster : EntityBase
    {
        private int _numberOfPlants = 1;

        /// <summary>
        /// Friendly name of the <see cref="PlantCluster"/>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Number of plants in this <see cref="PlantCluster"/>
        /// </summary>
        public int NumberOfPlants
        {
            get => _numberOfPlants;
            set => _numberOfPlants = value < 1 ? 1 : value;
        }

        /// <summary>
        ///  Gets or sets the foreign key property of the related <see cref="PlantFamily"/>
        /// </summary>
        public long? FamilyId { get; set; }

        /// <summary>
        /// The type of plant in this <see cref="PlantCluster"/>
        /// </summary>
        public PlantFamily Family { get; set; }

        /// <summary>
        /// Gets or sets the foreign key property of the related <see cref="SensorDevice"/>
        /// </summary>
        public long? SensorUnitId { get; set; }

        /// <summary>
        /// The <see cref="SensorDevice"/> measuring the cluster environment
        /// </summary>
        public SensorDevice SensorUnit { get; set; }

        /// <summary>
        /// Gets or sets the foreign key property of the related <see cref="ActorDevice"/>
        /// </summary>
        public long? ActorUnitId { get; set; }

        /// <summary>
        /// The <see cref="ActorDevice"/> controlling the cluster environment
        /// </summary>
        public ActorDevice ActorUnit { get; set; }
    }
}
