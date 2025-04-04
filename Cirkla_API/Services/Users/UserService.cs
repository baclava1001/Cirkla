using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.UoW;
using Cirkla_DAL.Repositories.Users;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API.Services.Users
{
    public class UserService(
        IUserRepository userRepository,
        IContractRepository contractRepository,
        IUnitOfWork unitOfWork,
        ILogger<UserService> logger) : IUserService
    {
        public async Task<ServiceResult<string>> Create(User user)
        {
            if (user is null)
            {
                logger.LogWarning("Attempted creating a user with null value");
                return ServiceResult<string>.Fail("User is null", ErrorType.ValidationError);
            }

            var createdUser = await userRepository.Create(user);
            await unitOfWork.SaveChanges();
            return ServiceResult<string>.Created(createdUser.Id);
        }


        public async Task<ServiceResult<object>> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                logger.LogWarning("ID value is null or empty");
                return ServiceResult<object>.Fail("Invalid ID", ErrorType.ValidationError);
            }

            if (!await CanDelete(id))
            {
                logger.LogWarning("Attempted to delete user with ID {UserId} who has active borrowings or sharings",
                    id);
                return ServiceResult<object>.Fail("Unable to delete users with active", ErrorType.ValidationError);
            }

            var user = await userRepository.Get(id);
            if (user is null)
            {
                logger.LogWarning("Attempted to delete a non-existent user with ID {UserId}", id);
                return ServiceResult<object>.Fail("User not found", ErrorType.NotFound);
            }

            await userRepository.Delete(user);
            await unitOfWork.SaveChanges();
            return ServiceResult<object>.Success(null);
        }


        public async Task<ServiceResult<User>> GetById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                logger.LogWarning("ID value is null or empty");
                return ServiceResult<User>.Fail("Invalid ID", ErrorType.ValidationError);
            }

            var user = await userRepository.Get(id);
            if (user is null)
            {
                logger.LogWarning("User with ID {UserId} not found", id);
                return ServiceResult<User>.Fail("User not found", ErrorType.NotFound);
            }

            return ServiceResult<User>.Success(user);
        }

        public async Task<ServiceResult<IEnumerable<User>>> GetAll()
        {
            var users = await userRepository.GetAll();
            if (users is null)
            {
                logger.LogWarning("No users found");
                return ServiceResult<IEnumerable<User>>.Fail("No users found", ErrorType.NotFound);
            }

            return ServiceResult<IEnumerable<User>>.Success(users);
        }

        public async Task<ServiceResult<object>> Update(string id, User user)
        {
            if (string.IsNullOrEmpty(id) || user.Id != id)
            {
                logger.LogWarning("User with ID {UserId} not found", id);
                return ServiceResult<object>.Fail("User not found", ErrorType.ValidationError);
            }

            var updatedUser = await userRepository.Update(user);
            await unitOfWork.SaveChanges();
            return ServiceResult<object>.Success(null);
        }


        #region Helpers

        private async Task<bool> CanDelete(string id)
        {
            var activeBorrowings = await contractRepository.GetActiveWhereUserIsBorrower(id);
            var activeSharings = await contractRepository.GetActiveWhereUserIsOwner(id);

            if (activeBorrowings.Any() || activeSharings.Any())
            {
                logger.LogWarning("Attempted to delete user with ID {UserId} who has active borrowings or sharings",
                    id);
                return false;
            }
            return true;
        }

        #endregion
    }
}
