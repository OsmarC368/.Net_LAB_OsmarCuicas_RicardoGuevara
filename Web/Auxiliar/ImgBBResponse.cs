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
    public string? Url { get; set; }           
    public string? DisplayUrl { get; set; }
    public string? DeleteUrl { get; set; }     
    public Thumb? Thumb { get; set; }          

    }

public class Thumb
{
    public string? Filename { get; set; }
    public string? Name { get; set; }
    public string? Url { get; set; }
}
}