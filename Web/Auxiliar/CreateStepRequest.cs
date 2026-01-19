using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Auxiliar
{
    public class CreateStepRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public float Duration { get; set; }
        public string imageURL { get; set; } = string.Empty;
        
        public IFormFile? ImageFile { get; set; }
    }
}