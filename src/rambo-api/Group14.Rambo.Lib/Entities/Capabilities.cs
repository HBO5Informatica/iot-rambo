namespace Group14.Rambo.Lib.Entities
{
    using System;
    /// <summary>
    /// Defines the capabilities of a <see cref="ActorDevice"/>
    /// </summary>
    [Flags]
    public enum ActorCapabilities : byte
    {
        /// <summary>
        /// Can't do anything
        /// </summary>
        None = 0,
        /// <summary>
        /// Can add water to environment
        /// </summary>
        AddWater = 1,
        /// <summary>
        /// Can adjust environment lighting up or down
        /// </summary>
        AdjustLight = 2,
        /// <summary>
        /// Can adjust environment temperature up or down
        /// </summary>
        AdjustHeat = 4
    }

    /// <summary>
    /// Defines the capabilities of a <see cref="SensorDevice"/>
    /// </summary>
    [Flags]
    public enum SensorCapabilities : byte
    {
        /// <summary>
        /// Can't do anything
        /// </summary>
        None = 0,
        /// <summary>
        /// Can measure soil humidity
        /// </summary>
        MoistureMeter = 1,
        /// <summary>
        /// Can measure air humidity
        /// </summary>
        Hygrometer = 2,
        /// <summary>
        /// Can measure air temperature
        /// </summary>
        Thermometer = 4,
        /// <summary>
        /// Can measure light intensity
        /// </summary>
        LuxMeter = 8
    }
}
