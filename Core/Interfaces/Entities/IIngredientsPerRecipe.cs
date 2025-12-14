using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces.Entities
{
    public interface IIngredientsPerRecipe
    {
        public int id { get; set; }
        public int RecipeId { get; set; }
        public int IngredientId { get; set; }
        public float amount { get; set; }
    }
}