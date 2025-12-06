using Core.Interfaces.Repositories;

namespace Core.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IUserRepository UserRepository { get; }

	Task<int> CommitAsync();
}