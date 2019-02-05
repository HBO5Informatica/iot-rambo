using System;

namespace Group14.Rambo.Lib.Dto
{
    using Entities;
    /// <summary>
    /// Defines all possible commands that can be send to a <see cref="ActorDevice"/>
    /// </summary>
    [Flags]
    public enum CommandType : byte
    {
        None = 0,
        AddWater = 0x01,
        AdjustHeat = 0x02,
        AdjustLight = 0x04,
        StopAdvertising = 0x80
    }
}
