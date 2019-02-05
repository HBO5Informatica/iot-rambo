namespace Group14.Rambo.Api.Repositories.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Data;
    using Lib.Entities;
    using Microsoft.EntityFrameworkCore;

    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        protected readonly RamboContext RamboContext;

        public Repository(RamboContext context)
        {
            RamboContext = context;
        }

        public virtual async Task<T> GetById(int id)
        {
            return await RamboContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<T> Add(T entity)
        {
            RamboContext.Set<T>().Add(entity);
            try
            {
                await RamboContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                return null;
            }

            return entity;
        }

        public virtual async Task<T> Delete(T entity)
        {
            RamboContext.Set<T>().Remove(entity);
            try
            {
                await RamboContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return null;
            }
            return entity;
        }

        public virtual async Task<T> Delete(int id)
        {
            var entity = await GetById(id);
            if (entity == null) return null;
            return await Delete(entity);
        }

        public virtual IQueryable<T> GetAll()
        {
            return RamboContext.Set<T>().AsNoTracking();
        }

        public virtual IQueryable<T> GetFiltered(Expression<Func<T, bool>> predicate)
        {
            return RamboContext.Set<T>().Where(predicate).AsNoTracking();
        }

        public virtual async Task<IEnumerable<T>> ListAll()
        {
            return await GetAll().ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> ListFiltered(Expression<Func<T, bool>> predicate)
        {
            return await GetFiltered(predicate).ToListAsync();
        }

        public virtual async Task<T> Update(T entity)
        {
            RamboContext.Entry(entity).State = EntityState.Modified;
            try
            {
                await RamboContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                return null;
            }
            return entity;
        }

        private async Task<bool> Exists(int id)
        {
            return await RamboContext.Set<T>().AnyAsync(e => e.Id == id);
        }
    }
}
