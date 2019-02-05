using Group14.Rambo.Lib.Helpers;

namespace Group14.Rambo.Web.Pages
{
    using System.Collections.Generic;
    using Lib.Entities;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;

    public class PlantFamilyModel : PageModel
    {
        public List<PlantFamily> Result { get; set; }

        private static string baseUri;

        public PlantFamilyModel(IConfiguration iConfiguration)
        {
            baseUri = iConfiguration.GetSection("ApiBaseUri").Value;
        }

        public void OnGet()
        {
            Result = WebApiHelper.GetApiResult<List<PlantFamily>>($"{baseUri}PlantFamily");
        }
    }
}