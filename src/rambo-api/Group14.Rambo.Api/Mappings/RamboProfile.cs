using Group14.Rambo.Lib.Dto;
using Group14.Rambo.Lib.Entities;

namespace Group14.Rambo.Api.Mappings
{
    using AutoMapper;

    public class RamboProfile: Profile
    {
        public RamboProfile()
        {
            CreateMap<SensorData, SensorRecord>();
        }
    }
}