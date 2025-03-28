namespace Cirkla_DAL.Repositories.UoW;

public interface IUnitOfWork
{
    Task<int> SaveChanges();
    Task<int> SaveChangesWithTransaction();
}