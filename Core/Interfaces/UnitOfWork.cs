using Core.Interfaces.Repositories;

namespace Core.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }
    IIngredientRepository IngredientRepository { get; }

	Task<int> CommitAsync();
}