namespace Group14.Rambo.Api.Repositories
{
    using Data;
    using Lib.Entities;
    using Base;

    public class ActorRepository : Repository<ActorDevice>
    {        
        public ActorRepository(RamboContext context) : base(context)
        {            
        }
    }
}
