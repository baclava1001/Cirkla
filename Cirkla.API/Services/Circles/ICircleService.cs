using Cirkla.API.Common;
using Cirkla.Data.Models;
using Cirkla.Shared.DTOs.Circles;

namespace Cirkla.API.Services.Circles;

public interface ICircleService
{
    Task<ServiceResult<int>> Create(CircleCreateDTO circleDTO);
    Task<ServiceResult<IEnumerable<Circle>>> GetAll();
    Task<ServiceResult<Circle>> GetById(int id);
    Task<ServiceResult<Circle>> Update(int id, Circle circle);
    Task<ServiceResult<Circle>> Delete(int id);
}