using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Circles;
using Cirkla_DAL.Repositories.Users;
using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.Circles
{
    public class CircleService : ICircleService
    {
        private readonly ICircleRepository _circleRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILogger<CircleService> _logger;

        public CircleService(ICircleRepository circleRepository, IUserRepository userRepository, ILogger<CircleService> logger)
        {
            _circleRepository = circleRepository;
            _userRepository = userRepository;
            _logger = logger;
        }


        public async Task<ServiceResult<Circle>> Create(Circle circle)
        {
            // TODO: Add more validation for: createdby, createdat, etc etc
            if (circle is null)
            {
                _logger.LogWarning("Attempted creating a circle with null value");
                return ServiceResult<Circle>.Fail("Circle could not be created", ErrorType.ValidationError);
            }

            try
            {
                var createdCircle = await _circleRepository.Create(circle);
                await _circleRepository.SaveChanges();
                return ServiceResult<Circle>.Success(createdCircle);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed writing new circle to database");
                return ServiceResult<Circle>.Fail("Error saving new circle", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating new circle");
                return ServiceResult<Circle>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<IEnumerable<Circle>>> GetAll()
        {
            try
            {
                IEnumerable<Circle> circles = await _circleRepository.GetAll();
                if (!circles.Any())
                {
                    _logger.LogWarning("No Circles found in database");
                    return ServiceResult<IEnumerable<Circle>>.Fail("No circles found", ErrorType.NotFound);
                }
                return ServiceResult<IEnumerable<Circle>>.Success(circles);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting all circles");
                return ServiceResult<IEnumerable<Circle>>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<Circle>> GetById(int id)
        {
            try
            {
                var circle = await _circleRepository.GetById(id);
                if (circle is null)
                {
                    _logger.LogWarning("Circle with ID {CircleId} not found", id);
                    return ServiceResult<Circle>.Fail("Circle not found", ErrorType.NotFound);
                }
                return ServiceResult<Circle>.Success(circle);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting circle with id {CircleId}", id);
                return ServiceResult<Circle>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<Circle>> Update(int id, Circle circle)
        {
            if (circle is null)
            {
                _logger.LogWarning("Attempted to update a non-existent circle with ID {CircleId}", id);
                ServiceResult<Circle>.Fail("Circle could not be updated", ErrorType.ValidationError);
            }

            if (id != circle.Id)
            {
                _logger.LogWarning("Circle ID {CircleId} does not match the ID in the request body", id);
                return ServiceResult<Circle>.Fail("Incorrect id", ErrorType.ValidationError);
            }

            try
            {
                var updatedCircle = await _circleRepository.Update(circle);
                await _circleRepository.SaveChanges();
                return ServiceResult<Circle>.Success(updatedCircle);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating circle with ID {CircleId}", id);
                return ServiceResult<Circle>.Fail("Internal error, could not update circle", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating circle with ID {CircleId}", id);
                return ServiceResult<Circle>.Fail("Internal server error", ErrorType.InternalError);
            }
        }


        public async Task<ServiceResult<Circle>> Delete(int id)
        {
            Circle circle = await _circleRepository.GetById(id);
            if (circle is null)
            {
                _logger.LogWarning("Attempted to delete a non-existent circle with ID {CircleId}", id);
                return ServiceResult<Circle>.Fail("Circle not found", ErrorType.NotFound);
            }

            try
            {
                var deletedCircle = await _circleRepository.Delete(circle);
                await _circleRepository.SaveChanges();
                return ServiceResult<Circle>.Success(deletedCircle);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting circle with ID {CircleId}", id);
                return ServiceResult<Circle>.Fail("Internal error, could not delete circle", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting circle with ID {CircleId}", id);
                return ServiceResult<Circle>.Fail("Internal server error", ErrorType.InternalError);
            }
        }
    }
}
