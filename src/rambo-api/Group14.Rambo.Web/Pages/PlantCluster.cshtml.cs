using Group14.Rambo.Lib.Helpers;

namespace Group14.Rambo.Web.Pages
{
    using System.Collections.Generic;
    using Lib.Entities;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.Extensions.Configuration;

    public class PlantClusterModel : PageModel
    {
        public List<PlantCluster> Result { get; set; }

        private static string baseUri;

        public PlantClusterModel(IConfiguration iConfiguration)
        {
            baseUri = iConfiguration.GetSection("ApiBaseUri").Value;
        }

        public void OnGet()
        {
            Result = WebApiHelper.GetApiResult<List<PlantCluster>>($"{baseUri}PlantCluster");
        }
    }
}