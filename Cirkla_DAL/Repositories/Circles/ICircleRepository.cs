using Cirkla_DAL.Models;

namespace Cirkla_DAL.Repositories.Circles;

public interface ICircleRepository
{
    Task<Circle> Create(Circle circle);
    Task<IEnumerable<Circle>> Getall();
    Task<Circle?> GetById(int id);
    Task<Circle> Update(Circle circle);
    Task<Circle> Delete(Circle circle);
    Task SaveChanges();
}