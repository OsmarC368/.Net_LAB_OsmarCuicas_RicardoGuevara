using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Auxiliar
{
    public class CreateRecipeRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int DifficultyLevel { get; set; }
        public int Visibility { get; set; }   
        public int Servings { get; set; }   
        public int UserIDR { get; set; }     
        public int UserRID { get; set; }     

        public IFormFile? ImageFile { get; set; }
    }
}