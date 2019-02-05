namespace Group14.Rambo.Lib.Dto
{
    using Entities;
    /// <summary>
    /// DTO object, routed through a <see cref="NodeDevice"/> with a <see cref="ActorDevice"/> as destination
    /// </summary>
    public class ActorCommand : DtoBase
    {
        /// <summary>
        /// HW address of the <see cref="NodeDevice"/>
        /// </summary>
        public string NodeAddress { get; set; }

        /// <summary>
        /// HW address of the destined <see cref="ActorDevice"/>
        /// </summary>
        public string ActorAddress { get; set; }

        /// <summary>
        /// Command is routed by this <see cref="NodeDevice"/>
        /// </summary>
        public NodeDevice Node { get; set; }

        /// <summary>
        /// Command is destined for this <see cref="ActorDevice"/>
        /// </summary>
        public ActorDevice Actor { get; set; }

        /// <summary>
        /// Defines what the <see cref="ActorDevice"/> should do
        /// </summary>
        public CommandType Command { get; set; }

        /// <summary>
        /// Water to adjust
        /// </summary>
        public float Water { get; set; }

        /// <summary>
        /// Temperature to adjust
        /// </summary>
        public float Temperature { get; set; }

        /// <summary>
        /// Light level to adjust
        /// </summary>
        public float LightLevel { get; set; }
    }
}
