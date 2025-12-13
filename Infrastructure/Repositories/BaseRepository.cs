using System;
using System.Collections.Generic;
using Core.Interfaces.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class BaseRepository<Entity> : IBaseRepository<Entity> where Entity : class
    {
        internal AppDbContext context;
        internal DbSet<Entity> dbSet;

        public BaseRepository(AppDbContext context)
        {
           this .context = context;
           this .dbSet = context.Set<Entity>();
        }

        public virtual async ValueTask<Entity> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<Entity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public virtual async void Remove(Entity entity)
        {
            dbSet.Remove(entity);
        }

        public virtual async void RemoveRange(IEnumerable<Entity> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual async Task Update(Entity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            await Task.CompletedTask; 
        }

        public virtual async Task<Entity> AddAsync(Entity entity) //Para que en teoría retorne la entidad que agregó. Esto para poder consultar que id autoincrementado asignó.
        {
            var entityEntry = await dbSet.AddAsync(entity);
            return entityEntry.Entity;
        }
    }
}
