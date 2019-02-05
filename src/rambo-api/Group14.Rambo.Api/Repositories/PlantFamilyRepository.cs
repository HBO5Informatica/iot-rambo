namespace Group14.Rambo.Api.Repositories
{
    using Data;
    using Base;
    using Lib.Entities;

    public class PlantFamilyRepository: Repository<PlantFamily>
    {        
        public PlantFamilyRepository(RamboContext context) : base(context)
        {            
        }
    }
}
