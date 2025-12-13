using Core.Entities;                      
using Core.Interfaces.Repositories;        
using Infrastructure.Data;                 
using Microsoft.EntityFrameworkCore;       

namespace Infrastructure.Repositories
{
    public class IngredientRepository : BaseRepository<Ingredient>, IIngredientRepository
    {
        public IngredientRepository(AppDbContext context) : base(context)
		{

		}
    }
}
