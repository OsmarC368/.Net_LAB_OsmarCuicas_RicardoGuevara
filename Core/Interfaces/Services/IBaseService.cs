using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces.Services
{
    public interface IBaseService<Entity> where Entity : class
    {
        Task<Entity> GetById(int id);
        Task<IEnumerable<Entity>> GetAll();
        Task<Entity> Create(Entity newEntity);
        Task<Entity> Update(int entityId, Entity newEntityValues);
        Task Delete(int entityId);
    }
}