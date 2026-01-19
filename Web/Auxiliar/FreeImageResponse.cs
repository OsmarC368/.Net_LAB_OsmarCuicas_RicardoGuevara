using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Auxiliar
{
    public class FreeImageResponse
    {
        public FreeImageData? Image { get; set; }
        public int Status { get; set; }
        public bool Success { get; set; }
    }

    public class FreeImageData
{
    public string? Url { get; set; }         // link directo a la imagen
    public string? Name { get; set; }
    // Puedes agregar: Thumb?.Url, Medium?.Url, etc. si los usas
}
}