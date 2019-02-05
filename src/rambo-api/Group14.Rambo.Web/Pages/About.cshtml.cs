using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Group14.Rambo.Lib.Entities;
using Group14.Rambo.Lib.Helpers;
using Microsoft.Extensions.Configuration;

namespace Group14.Rambo.Web.Pages
{
    public class AboutModel : PageModel
    {
        public List<ActorDevice> Result { get; set; }

        private static string baseUri;

        public AboutModel(IConfiguration iConfiguration)
        {
            baseUri = iConfiguration.GetSection("ApiBaseUri").Value;
        }

        public void OnGet()
        {
            
            Result = WebApiHelper.GetApiResult<List<ActorDevice>>($"{baseUri}actors");
        }
    }
}
