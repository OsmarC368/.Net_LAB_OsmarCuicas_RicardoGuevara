using Core.Interfaces;
using Core.Interfaces.Repositories;
using Infrastructure.Repositories; 

namespace Infrastructure.Data;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context;
    private IUserRepository? _userRepository;
    private IIngredientRepository? _ingredientRepository;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);
    public IIngredientRepository IngredientRepository => _ingredientRepository ??= new IngredientRepository(_context);

    public async Task<int> CommitAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
