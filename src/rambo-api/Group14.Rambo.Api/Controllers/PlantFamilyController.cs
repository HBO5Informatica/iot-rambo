namespace Group14.Rambo.Api.Controllers
{
    using Repositories;
    using Lib.Entities;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class PlantFamilyController : ControllerCrudBase<PlantFamily, PlantFamilyRepository>
    {
        public PlantFamilyController(PlantFamilyRepository plantFamilyRepository): base(plantFamilyRepository)
        {            
        }
    }
}