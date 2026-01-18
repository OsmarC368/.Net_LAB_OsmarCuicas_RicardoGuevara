using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces.Repositories;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class StepRepository : BaseRepository<Step>, IStepRepository
    {
        public StepRepository(AppDbContext context) : base(context){}
        
    }
}