using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        internal AppDbContext context;
        internal DbSet<TEntity> dbSet;

        public BaseRepository(AppDbContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        public virtual async ValueTask<TEntity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async void Remove(TEntity entity)
        {
            dbSet.Remove(entity);
        }

        public virtual async void RemoveRange(IEnumerable<TEntity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual async Task Update(TEntity entityUpdated)
        {
            dbSet.Attach(entityUpdated);
            context.Entry(entityUpdated).State = EntityState.Modified;
        }

        public virtual async Task UpdateRange(IEnumerable<TEntity> entitiesUpdated)
        {
            dbSet.AttachRange(entitiesUpdated);
            context.Entry(entitiesUpdated).State = EntityState.Modified;
        }

        public virtual async Task AddAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
        }
    }
}