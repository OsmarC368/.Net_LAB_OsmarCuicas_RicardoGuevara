using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Interfaces.Entities
{
    public interface IStepUser
    {
        public int id { get; set; } 
        public bool completed { get; set; } 
        public string comment { get; set; } 
    }
}