namespace Group14.Rambo.Api.Repositories
{
    using Data;
    using Base;
    using Lib.Entities;

    public class SensorDeviceRepository: Repository<SensorDevice>
    {        
        public SensorDeviceRepository(RamboContext context) : base(context)
        {            
        }
    }
}
