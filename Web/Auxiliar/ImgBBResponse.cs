using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Auxiliar
{
    public class ImgBBResponse
    {
        public bool Success { get; set; }
        public int Status { get; set; }
        public ImgBBData? Data { get; set; }
    }
    public class ImgBBData
    {
    public string? Id { get; set; }
    public string? Title { get; set; }
    public string? Url { get; set; }           // ← URL directa full size (usa esta)
    public string? DisplayUrl { get; set; }
    public string? DeleteUrl { get; set; }     // para borrar si necesitas
    public Thumb? Thumb { get; set; }          // thumbnail si lo quieres
    // ... más campos si necesitas
    }

public class Thumb
{
    public string? Filename { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
}
}