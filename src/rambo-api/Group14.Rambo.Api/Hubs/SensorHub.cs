namespace Group14.Rambo.Api.Hubs
{
    using System.Threading.Tasks;
    using Constants;
    using Microsoft.AspNetCore.SignalR;

    public class SensorHub : Hub
    {
        //public async Task UpdateSoilValue(string newValue)
        //{
        //    await Clients.All.SendAsync(HubConstants.SoilValueUpdate, newValue);
        //}

        //public async Task UpdateHumidityValue(string newValue)
        //{
        //    await Clients.All.SendAsync(HubConstants.HumidityValueUpdate, newValue);
        //}

        //public async Task UpdateTemperatureValue(string newValue)
        //{
        //    await Clients.All.SendAsync(HubConstants.TemperatureValueUpdate, newValue);
        //}

        //public async Task UpdateLightLevelValue(string newValue)
        //{
        //    await Clients.All.SendAsync(HubConstants.LightLevelValueUpdate, newValue);
        //}
    }
}
