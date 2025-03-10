using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.Circles;

public interface ICircleService
{
    Task<ServiceResult<Circle>> Create(Circle circle);
    Task<ServiceResult<IEnumerable<Circle>>> GetAll();
    Task<ServiceResult<Circle>> GetById(int id);
    Task<ServiceResult<Circle>> Update(int id, Circle circle);
    Task<ServiceResult<Circle>> Delete(int id);
}