namespace Group14.Rambo.Api.Repositories
{
    using Data;
    using Base;
    using Lib.Entities;

    public class NodeDeviceRepository: Repository<NodeDevice>
    {        
        public NodeDeviceRepository(RamboContext context) : base(context)
        {            
        }
    }
}
