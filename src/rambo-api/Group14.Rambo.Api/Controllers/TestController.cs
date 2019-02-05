using System.Threading.Tasks;
using Group14.Rambo.Api.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Group14.Rambo.Api.Controllers
{
    using System.Collections.Generic;
    using Lib.Dto;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    public class TestController : Controller
    {
        private readonly IHubContext<MainHub> _hubContext;
        public TestController(IHubContext<MainHub> hubcontext)
        {
            _hubContext = hubcontext;
        }
        
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new[] { "Bart", "Soete" };
        }

        [HttpGet]
        [Route("Hub")]
        public async Task<string> TriggerHub()
        {
            await _hubContext.Clients.All.SendAsync("TestMessage", "Hub was triggered");
            return "Hub Triggered";
        }


        [HttpGet]
        [Route("ActorCmd")]
        public async Task<ActorCommand> DispatchActorCommand()
        {
            var command = new ActorCommand
            {
                Command = CommandType.AdjustLight | CommandType.AddWater,
                NodeAddress = "060606070707",
                ActorAddress = "721B9B190000",
                LightLevel = 2.5f,
                Temperature = 10.5f,
                Water = 0.5f
            };
            await _hubContext.Clients.All.SendAsync("ActorCommand", command);
            return command;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return id.ToString();
        }

   
        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
