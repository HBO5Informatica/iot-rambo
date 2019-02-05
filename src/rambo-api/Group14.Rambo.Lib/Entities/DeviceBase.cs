namespace Group14.Rambo.Lib.Entities
{
    /// <inheritdoc />
    /// <summary>
    /// Common base class for a registered device
    /// </summary>
    public abstract class DeviceBase : EntityBase
    {
        /// <summary>
        /// The unique hardware address of this device
        /// </summary>
        public string HardwareAddress { get; set; }

        /// <summary>
        /// The optional user defined name of this device
        /// </summary>
        public string Name { get; set; }
    }
}
