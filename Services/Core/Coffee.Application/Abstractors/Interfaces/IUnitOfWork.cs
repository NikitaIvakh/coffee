namespace Coffee.Application.Abstractors.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken token);
}