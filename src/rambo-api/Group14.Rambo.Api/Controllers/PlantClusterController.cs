namespace Group14.Rambo.Api.Controllers
{
    using Repositories;
    using Lib.Entities;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [ApiController]
    public class PlantClusterController : ControllerCrudBase<PlantCluster, PlantClusterRepository>
    {
        public PlantClusterController(PlantClusterRepository plantClusterRepository): base(plantClusterRepository)
        {            
        }
    }
}