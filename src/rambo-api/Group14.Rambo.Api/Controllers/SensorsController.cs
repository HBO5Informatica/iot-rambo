using System;
using Group14.Rambo.Api.Constants;
using Group14.Rambo.Api.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Group14.Rambo.Api.Controllers
{
    using System.Threading.Tasks;
    using Repositories;
    using Lib.Dto;
    using Lib.Entities;
    using Microsoft.AspNetCore.Mvc;
    using AutoMapper;

    [Route("api/[controller]")]
    public class SensorsController : ControllerCrudBase<DataTest,TestRepository>
    {
        private readonly TestRepository _testRepository;
        private readonly IMapper _mapper;
        private readonly IHubContext<SensorHub> _hubContext;

        public SensorsController(TestRepository testRepository, IMapper mapper, IHubContext<SensorHub> hubContext) : base(testRepository)
        {
            _testRepository = testRepository;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        // GET: api/<controller>
        [HttpGet]
        public override async Task<IActionResult> Get()
        {
            var randomData = await _testRepository.GetAll().FirstAsync();
            return new ObjectResult(JsonConvert.DeserializeObject<SensorData>(randomData.ApiData));
        }
        
        // POST api/<controller>
        [HttpPost]
        [Route("New")]
        public async Task<IActionResult> Post([FromBody]SensorData testData, int id)
        {
            //Save as JSON to tempdatabase
            //_ramboContext.Set<DataTest>().Add(new DataTest {ApiData = testData.ToJsonString()});
            //await _ramboContext.SaveChangesAsync();
            
            //Process sensorRecord
            var sensorRecord = _mapper.Map<SensorRecord>(testData);
            sensorRecord.RegisteredDateTime = DateTime.Now;
            sensorRecord.SensorDevice = await _testRepository.GetSensorDevice(testData.SensorAddress);
            await _testRepository.AddSensorRecord(sensorRecord);
            
            await _hubContext.Clients.All.SendAsync(HubConstants.SoilValueUpdate, sensorRecord.SoilMoisture);
            await _hubContext.Clients.All.SendAsync(HubConstants.HumidityValueUpdate, sensorRecord.Humidity);
            await _hubContext.Clients.All.SendAsync(HubConstants.TemperatureValueUpdate, sensorRecord.Temperature);
            await _hubContext.Clients.All.SendAsync(HubConstants.LightLevelValueUpdate, sensorRecord.LightLevel);

            return new ObjectResult(sensorRecord);
        }
    }
}


/*
 *### 2.2.3 - Sensor Data - Datagram

When the sensor broadcasts the device information and measurements, it structures this information using the `SensorData` structure

`D MM C SS HHHH TTTT LLLL NNNNNN` (24 Bytes)

| byte index    | Length (B)   | Type      | Description                               |
|:-------------:|:------------:| --------- | ----------------------------------------- |
| 0             | 1            | `byte`    | `D` = `DTYPE_SENSORDATA`, always `0x01` * |
| 1             | 2            | `word`    | `M` = Message ID **                       |
| 3             | 1            | `byte`    | `C` = Capabilties (flags)                 |
| 4             | 2            | `int`     | `S` = Soil moisture                       |
| 6             | 4            | `float`   | `H` = Humidity                            |
| 10            | 4            | `float`   | `T` = Temperature (C)                     |
| 14            | 4            | `float`   | `L` = Light level (LDR Ω)                 |
| 18            | 6            | `byte[6]` | `N` = 24 bits Source BO-Sensor ID         |

All numbers (floats and ints) are byte-arranged in the native Arduino machine format, which is  **Little Endian**.

\* The first byte is the `Dtype`, which occurs in every RamboTalk datagram, identifies the message as SensorData. SensorData always has a `Dtype` of `0x01`.

\** The message ID are sequential 16-bit numbers which may be used by the API to distinguish duplicate messages from the same device and may enable a rough estimate of packet loss.

For example, when a BO-Sensor broadcasts this byte array (HEX):

`01 02 00 0F 2C 01 66 66 E6 3E 00 00 AC 41 66 66 48 43 01 00 08 00 19 FF`

This means the following:

- `01` = This message is SensorData
- `02 00` = Message Id equals 2
- `0F` = 15 (moisture, hygro-, thermo-, and luxmeter capabilities)
- `2C 01` = 300 (Soil moisture)
- `66 66 E6 3E` = 0.45 (Humidity)
- `00 00 AC 41` = 21.50 (Temperature)
- `66 66 48 43` = 200.40 (Light level)
- `01 00 08 00 19 FF` = [exact binary] (Sensor Unique ID)
 *
 *
 */