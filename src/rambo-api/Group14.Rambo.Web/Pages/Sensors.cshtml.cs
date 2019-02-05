using Group14.Rambo.Lib.Helpers;

namespace Group14.Rambo.Web.Pages
{
    using System.Collections.Generic;
    using Lib.Entities;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;

    public class SensorsModel : PageModel
    {
        public List<SensorRecord> Result { get; set; }
        public string SensorName { get; set; }

        private static string baseUri;

        public SensorsModel(IConfiguration iConfiguration)
        {
            baseUri = iConfiguration.GetSection("ApiBaseUri").Value;
        }

        public void OnGet(int id)
        {
            SensorName = "Rambo SensorDevice";
            Result = WebApiHelper.GetApiResult<List<SensorRecord>>($"{baseUri}SensorRecords");
        }
    }
}


