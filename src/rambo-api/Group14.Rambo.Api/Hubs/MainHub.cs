namespace Group14.Rambo.Api.Hubs
{
    using Microsoft.AspNetCore.SignalR;
    using System.Threading.Tasks;

    public class MainHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("TestMessage", message);
        }
    }
}
