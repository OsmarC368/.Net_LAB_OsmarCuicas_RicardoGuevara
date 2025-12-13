using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class MeasureRepository : BaseRepository<Measure>, IMeasureRepository
    {
        public MeasureRepository(AppDbContext context) : base(context) {}
        
    }
}