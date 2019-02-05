using Microsoft.EntityFrameworkCore;

namespace Group14.Rambo.Api.Repositories
{
    using Data;
    using Base;
    using Lib.Entities;
    using System.Threading.Tasks;

    public class TestRepository : Repository<DataTest>
    {
        public TestRepository(RamboContext ramboContext) : base(ramboContext)
        {
        }

        internal async Task<SensorDevice> GetSensorDevice(string sensorAddress)
        {
            return await RamboContext.SensorDevices.FirstOrDefaultAsync(sd => sd.HardwareAddress == sensorAddress);
        }

        public async Task AddSensorRecord(SensorRecord sensorRecord)
        {
            RamboContext.SensorRecords.Add(sensorRecord);
            await RamboContext.SaveChangesAsync();
        }
    }
}
