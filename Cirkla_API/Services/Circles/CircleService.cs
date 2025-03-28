﻿using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Circles;
using Cirkla_DAL.Repositories.UoW;
using Cirkla_DAL.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.Circles
{
    /// <summary>
    /// Simple CRUD for circles. Members and admins are handled in the CircleMemberService.
    /// </summary>
    public class CircleService : ICircleService
    {
        private readonly ICircleRepository _circleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CircleService> _logger;

        public CircleService(ICircleRepository circleRepository, IUnitOfWork unitOfWork, ILogger<CircleService> logger)
        {
            _circleRepository = circleRepository;
            _unitOfWork = unitOfWork;
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
                await _unitOfWork.SaveChanges();
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


            var updatedCircle = await _circleRepository.Update(circle);
            await _unitOfWork.SaveChanges();
            return ServiceResult<Circle>.Success(updatedCircle);
        }


        // TODO: DB-relation to circle requests stops this from working
        public async Task<ServiceResult<Circle>> Delete(int id)
        {
            Circle circle = await _circleRepository.GetById(id);
            if (circle is null)
            {
                _logger.LogWarning("Attempted to delete a non-existent circle with ID {CircleId}", id);
                return ServiceResult<Circle>.Fail("Circle not found", ErrorType.NotFound);
            }

            var deletedCircle = await _circleRepository.Delete(circle);
            await _unitOfWork.SaveChanges();
            return ServiceResult<Circle>.Success(deletedCircle);
        }
    }
}
