namespace Group14.Rambo.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using Lib.Entities;

    [Route("api/[controller]")]
    public class SensorRecordsController : ControllerCrudBase<SensorRecord,SensorRecordRepository>
    {
        private readonly SensorRecordRepository _sensorRecordRepository;
        
        public SensorRecordsController(SensorRecordRepository sensorRecordRepository) : base(sensorRecordRepository)
        {
        }
    }
}
