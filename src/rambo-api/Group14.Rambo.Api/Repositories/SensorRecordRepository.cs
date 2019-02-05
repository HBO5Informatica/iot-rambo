namespace Group14.Rambo.Api.Repositories
{
    using Data;
    using Base;
    using Lib.Entities;

    public class SensorRecordRepository: Repository<SensorRecord>
    {        
        public SensorRecordRepository(RamboContext context) : base(context)
        {            
        }
    }
}
