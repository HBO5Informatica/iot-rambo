namespace Group14.Rambo.Api.Controllers
{
    using System.Threading.Tasks;
    using Lib.Entities;
    using Microsoft.AspNetCore.Mvc;
    using Repositories.Base;

    public class ControllerCrudBase<T, TR> : ControllerBase where T : EntityBase where TR : Repository<T>
    {
        protected TR Repository;

        public ControllerCrudBase(TR r)
        {
            Repository = r;
        }

        //GET: api/T
        [HttpGet]
        public virtual async Task<IActionResult> Get()
        {
            return Ok(await Repository.ListAll());
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Get(int id)
        {
            return Ok(await Repository.GetById(id));
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Put([FromRoute] int id, [FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != entity.Id)
            {
                return BadRequest();
            }
            T updatedEntity = await Repository.Update(entity);
            if (updatedEntity == null)
            {
                return NotFound();
            }
            return Ok(updatedEntity);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Post([FromBody] T entity)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await Repository.Add(entity);

            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var entity = await Repository.Delete(id);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
    }
}
