namespace Group14.Rambo.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Repositories;
    using Lib.Entities;

    [Route("api/[controller]")]
    public class NodeDeviceController : ControllerCrudBase<NodeDevice, NodeDeviceRepository>
    {
        public NodeDeviceController(NodeDeviceRepository nodeDeviceRepository) : base(nodeDeviceRepository)
        {
        }
    }
}
