namespace Identity.Application.Abstractors.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken token = default);
}