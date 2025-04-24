using Azure.Core;
using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.CircleJoinRequests;
using Cirkla_DAL.Repositories.Circles;
using Cirkla_DAL.Repositories.UoW;
using Cirkla_DAL.Repositories.Users;
using Mapping.DTOs.Circles;
using Mapping.Mappers;
using Mapping.Validators.Circles;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.Circles
{
    /// <summary>
    /// Simple CRUD for circles. Members and admins are handled in the CircleMemberService.
    /// </summary>
    public class CircleService(ICircleRepository circleRepository,
                                IUserRepository userRepository,
                                IUnitOfWork unitOfWork,
                                CircleCreateValidator circleCreateValidator,
                                ILogger<CircleService> logger) : ICircleService
    {

        public async Task<ServiceResult<int>> Create(CircleCreateDTO circleDTO)
        {
            var validationResult = await circleCreateValidator.ValidateAsync(circleDTO);
            if (!validationResult.IsValid)
            {
                logger.LogWarning("Circle creation failed due to validation errors: {Errors}", validationResult.Errors);
                return ServiceResult<int>.Fail("Circle could not be created", ErrorType.ValidationError);
            }

            var circle = await Mapper.MapToCircle(circleDTO);
            var creatingUser = await userRepository.Get(circleDTO.CreatedById);
            if (creatingUser is null)
            {
                logger.LogWarning("Circle creation failed: User with ID {UserId} not found", circleDTO.CreatedById);
                return ServiceResult<int>.Fail("Circle could not be created", ErrorType.ValidationError);
            }
            circle.CreatedBy = creatingUser;
            circle.Administrators.Add(creatingUser);
            circle.Members.Add(creatingUser);   

            await circleRepository.Create(circle);
            await unitOfWork.SaveChanges();
            return ServiceResult<int>.Created(circle.Id);
        }


        public async Task<ServiceResult<IEnumerable<Circle>>> GetAll()
        {
            IEnumerable<Circle> circles = await circleRepository.GetAll();
            if (!circles.Any())
            {
                logger.LogWarning("No Circles found in database");
                return ServiceResult<IEnumerable<Circle>>.Fail("No circles found", ErrorType.NotFound);
            }
            return ServiceResult<IEnumerable<Circle>>.Success(circles);
        }


        public async Task<ServiceResult<Circle>> GetById(int id)
        {
            var circle = await circleRepository.GetById(id);
            if (circle is null)
            {
                logger.LogWarning("Circle with ID {CircleId} not found", id);
                return ServiceResult<Circle>.Fail("Circle not found", ErrorType.NotFound);
            }
            return ServiceResult<Circle>.Success(circle);
        }


        public async Task<ServiceResult<Circle>> Update(int id, Circle circle)
        {
            if (circle is null)
            {
                logger.LogWarning("Attempted to update a non-existent circle with ID {CircleId}", id);
                ServiceResult<object>.Fail("Circle could not be updated", ErrorType.ValidationError);
            }

            if (id != circle.Id)
            {
                logger.LogWarning("Circle ID {CircleId} does not match the ID in the request body", id);
                return ServiceResult<Circle>.Fail("Incorrect id", ErrorType.ValidationError);
            }


            var updatedCircle = await circleRepository.Update(circle);
            await unitOfWork.SaveChanges();
            return ServiceResult<Circle>.Success(updatedCircle);
        }


        // TODO: DB-relation to circle requests stops this from working
        public async Task<ServiceResult<Circle>> Delete(int id)
        {
            Circle circle = await circleRepository.GetById(id);
            if (circle is null)
            {
                logger.LogWarning("Attempted to delete a non-existent circle with ID {CircleId}", id);
                return ServiceResult<Circle>.Fail("Circle not found", ErrorType.NotFound);
            }

            // TODO: Fetch all related circle join requests and delete them

            await circleRepository.Delete(circle);
            await unitOfWork.SaveChanges();
            return ServiceResult<Circle>.Success(circle);
        }
    }
}
