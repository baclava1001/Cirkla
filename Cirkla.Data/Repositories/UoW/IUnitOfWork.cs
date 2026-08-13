namespace Cirkla.Data.Repositories.UoW;

public interface IUnitOfWork
{
    Task<int> SaveChanges();
    Task<int> SaveChangesWithTransaction();
}