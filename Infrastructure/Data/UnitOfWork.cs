using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories;

namespace Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private UserTypeRepository _userTypeRepository;
        private MeasureRepository _measureRepository;

        public UnitOfWork(AppDbContext context) { this._context = context; }

        public IUserTypeRepository UserTypeRepository => _userTypeRepository ??= new UserTypeRepository(_context);
        public IMeasureRepository MeasureRepository => _measureRepository ??= new MeasureRepository(_context);

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}