namespace Group14.Rambo.Api.Repositories
{
    using Data;
    using Base;
    using Lib.Entities;

    public class PlantClusterRepository: Repository<PlantCluster>
    {        
        public PlantClusterRepository(RamboContext context) : base(context)
        {            
        }
    }
}