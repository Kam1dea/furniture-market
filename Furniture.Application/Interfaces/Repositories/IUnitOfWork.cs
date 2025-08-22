namespace Furniture.Application.Interfaces.Repositories;

public interface IUnitOfWork
{
    Task<int> SaveAsync(CancellationToken ct = default);
}