using Cirkla_API.Common;
using Cirkla_DAL.Models;
using Mapping.DTOs.Circles;

namespace Cirkla_API.Services.Circles;

public interface ICircleService
{
    Task<ServiceResult<int>> Create(CircleCreateDTO circleDTO);
    Task<ServiceResult<IEnumerable<Circle>>> GetAll();
    Task<ServiceResult<Circle>> GetById(int id);
    Task<ServiceResult<object>> Update(int id, Circle circle);
    Task<ServiceResult<object>> Delete(int id);
}