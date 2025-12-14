using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class IngredientsPerRecipeRepository : BaseRepository<IngredientsPerRecipe>, IIngredientsPerRecipeRepository
    {
        public IngredientsPerRecipeRepository(AppDbContext context) : base(context){}
        
    }
}