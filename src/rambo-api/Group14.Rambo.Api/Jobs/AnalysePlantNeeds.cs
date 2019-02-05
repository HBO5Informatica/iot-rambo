using System.Collections.Generic;
using Group14.Rambo.Lib.Dto;
using Group14.Rambo.Lib.Entities;
using Group14.Rambo.Lib.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Group14.Rambo.Api.Jobs
{
    using System.Threading.Tasks;
    using Data;
    using Microsoft.Extensions.Configuration;
    using Quartz;
    using System;
    using System.Linq;
    using Constants;
    using Microsoft.EntityFrameworkCore;

    public class AnalyzePlantNeeds : IJob
    {
        private RamboContext _ramboContext;
        private readonly IConfiguration _configuration;
        private readonly string _baseUri;
        private bool _canRun;


        public AnalyzePlantNeeds(IConfiguration configuration, RamboContext ramboContext)
        {
            _configuration = configuration;
            _ramboContext = ramboContext;
            _baseUri = _configuration.GetSection("ApiBaseUri").Value;
            _canRun = true;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            if (_canRun)
            {
                _canRun = false;

                if (_ramboContext.AnalyticSteps.Any(a => a.Name == JobConstants.AnalyzePlantNeedsJob))
                {
                    var lastCheck = await _ramboContext.AnalyticSteps.SingleOrDefaultAsync(a => a.Name == JobConstants.AnalyzePlantNeedsJob);
                    lastCheck.DateTimeChecked = DateTime.Now;
                    _ramboContext.Update(lastCheck);
                    await _ramboContext.SaveChangesAsync();
                    await VerifyRecordWithOptimalParameters(lastCheck.DateTimeChecked);

                }
                else
                {
                    var newLastCheck = new AnalyticStep
                    {
                        Name = JobConstants.AnalyzePlantNeedsJob,
                        DateTimeChecked = DateTime.Now
                    };
                    _ramboContext.Add(newLastCheck);
                    await _ramboContext.SaveChangesAsync();
                    await VerifyRecordWithOptimalParameters(null);
                }

                //TODO: Update code with analytics for known data.
                //TODO: Push notifications based on analytics.


                /*
                 * TEST Job, can be evaluated in db SensorRecords,
                 * Updates RegisteredDateTime every 5 seconds              
                 */

                //var test = await _ramboContext.SensorRecords.FirstOrDefaultAsync();
                //test.RegisteredDateTime = DateTime.Now;
                //_ramboContext.SensorRecords.Update(test);
                //await _ramboContext.SaveChangesAsync();
            }
        }

        private async Task VerifyRecordWithOptimalParameters(DateTime? dateTimeChecked)
        {
            SensorRecord recordToCheck;
            if (dateTimeChecked.HasValue)
            {
                recordToCheck = _ramboContext.SensorRecords.Where(s => s.RegisteredDateTime >= dateTimeChecked.GetValueOrDefault())
                    .OrderByDescending(s => s.RegisteredDateTime)
                    .FirstOrDefault();
            }
            else
            {
                recordToCheck = _ramboContext.SensorRecords.OrderByDescending(s => s.RegisteredDateTime)
                    .FirstOrDefault();
            }

            if (recordToCheck == null)
            {
                return;
            }

            var optimalParameters = _ramboContext.PlantFamilies.FirstOrDefault();
            var commandType = CommandType.None;
            
            /*Redundant at present time, possible H2O Vapor extension*/
            //if (recordToCheck.Humidity < optimalParameters.AirHumidity)
            //{
            //   commandType |= CommandType.AddWater;
            //}

            if (recordToCheck.SoilMoisture < optimalParameters.SoilMoisture)
            {
                //soil is not moist enough, need to ADD WATER
                commandType |= CommandType.AddWater;
            }
            if (recordToCheck.LightLevel < optimalParameters.LightIntensity)
            {
                commandType |= CommandType.AdjustLight;
            }
            if (recordToCheck.Temperature < optimalParameters.AirTemperature)
            {
                commandType |= CommandType.AdjustHeat;
            }
            
            //Send command(s) towards Actors
            await WebApiHelper.PostCallApi<OkObjectResult, CommandType>($"{_baseUri}Actors/Manual/{commandType}", commandType);
            //Set trigger back to ok to run
            _canRun = true;
        }
    }
}