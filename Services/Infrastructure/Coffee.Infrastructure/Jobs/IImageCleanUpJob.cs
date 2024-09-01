namespace Coffee.Infrastructure.Jobs;

public interface IImageCleanUpJob
{
    Task ProcessAsync();
}