using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.Users
{
    /// <summary>
    /// Simple CRUD-service for internal use in other classes
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ServiceResult<User>> Create(User user)
        {
            if (user == null)
            {
                _logger.LogWarning("Attempted creating a user with null value");
                return ServiceResult<User>.Fail("User is null", ErrorType.ValidationError);
            }

            try
            {
                var createdUser = await _userRepository.Create(user);
                await _userRepository.SaveChanges();
                return ServiceResult<User>.Success(createdUser);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Failed writing new user to database");
                return ServiceResult<User>.Fail("Error saving new user", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error creating new user");
                return ServiceResult<User>.Fail("Internal server error", ErrorType.InternalError);
            }
        }

        public async Task<ServiceResult<User>> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                _logger.LogWarning("Attempted to delete a non-existent user with ID {UserId} not found", id);
                return ServiceResult<User>.Fail("User not found", ErrorType.ValidationError);
            }

            // TODO: Check if there are any active contracts before allowing deletion!

            try
            {
                var user = await _userRepository.Get(id);
                if (user == null)
                {
                    return ServiceResult<User>.Fail("User not found", ErrorType.NotFound);
                }

                await _userRepository.Delete(user);
                await _userRepository.SaveChanges();

                return ServiceResult<User>.Success(user);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error deleting user with ID {UserId}", id);
                return ServiceResult<User>.Fail("Internal error, could not delete user", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error deleting user with ID {UserId}", id);
                return ServiceResult<User>.Fail("Internal server error", ErrorType.InternalError);
            }
        }

        public async Task<ServiceResult<User>> GetById(string id)
        {
            try
            {
                var user = await _userRepository.Get(id);
                if (user == null)
                {
                    _logger.LogWarning("User with ID {UserId} not found", id);
                    return ServiceResult<User>.Fail("User not found", ErrorType.NotFound);
                }
                return ServiceResult<User>.Success(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting user with ID {UserId}", id);
                return ServiceResult<User>.Fail("Internal server error", ErrorType.InternalError);
            }
        }

        public async Task<ServiceResult<IEnumerable<User>>> GetAll()
        {
            try
            {
                var users = await _userRepository.GetAll();
                if (users == null)
                {
                    _logger.LogWarning("No users found");
                    return ServiceResult<IEnumerable<User>>.Fail("No users found", ErrorType.NotFound);
                }
                return ServiceResult<IEnumerable<User>>.Success(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error getting all users");
                return ServiceResult<IEnumerable<User>>.Fail("Internal server error", ErrorType.InternalError);
            }
        }

        public async Task<ServiceResult<User>> Update(string id, User user)
        {
            if (string.IsNullOrEmpty(id) || user.Id != id)
            {
                _logger.LogWarning("User with ID {UserId} not found", id);
                return ServiceResult<User>.Fail("User not found", ErrorType.ValidationError);
            }

            try
            {
                var updatedUser = await _userRepository.Update(user);
                await _userRepository.SaveChanges();
                return ServiceResult<User>.Success(updatedUser);
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, "Error updating user with ID {UserId}", id);
                return ServiceResult<User>.Fail("Internal error, could not update user", ErrorType.InternalError);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error updating user with ID {UserId}", id);
                return ServiceResult<User>.Fail("Internal error", ErrorType.InternalError);
            }
        }
    }
}
