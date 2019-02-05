namespace Group14.Rambo.Api.Controllers
{
    using Repositories;
    using Lib.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Lib.Dto;
    using Microsoft.AspNetCore.SignalR;
    using Hubs;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ActorsController : ControllerCrudBase<ActorDevice,ActorRepository>
    {
        private readonly IHubContext<MainHub> _hubContext;
        public ActorsController(ActorRepository actorRepository, IHubContext<MainHub> hubcontext) : base(actorRepository)
        {
            _hubContext = hubcontext;
        }

        [Route("Manual/{type}")]
        public async Task<IActionResult> ManualCommand(CommandType type)
        {
            var command = new ActorCommand
            {
                Command = type,
                NodeAddress = "060606070707",
                ActorAddress = "721B9B190000",
                LightLevel = type.HasFlag(CommandType.AdjustLight) ? 5.1f : 0,
                Temperature = type.HasFlag(CommandType.AdjustHeat) ? 5.2f : 0,
                Water = type.HasFlag(CommandType.AdjustLight) ? 5.3f : 0
            };
            await _hubContext.Clients.All.SendAsync("ActorCommand", command);
            return Ok(command);
        }
    }
}
