using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_API.Services.TokenGenerator;
using Cirkla_DAL.Models;
using Mapping.DTOs.Users;
using Mapping.Mappers;
using Microsoft.AspNetCore.Identity;

namespace Cirkla_API.Services.Authentication;

public class CustomAuthenticationService(UserManager<User> userManager,
                                        ITokenService tokenService,
                                        ILogger<CustomAuthenticationService> logger) : ICustomAuthenticationService
{

    public async Task<ServiceResult<User>> Signup(UserSignupDTO userSignupDTO)
    {
        try
        {
            User user = await Mapper.MapToUser(userSignupDTO);
            if (user == null)
            {
                logger.LogError("Failed to map UserSignupDTO to User for {Email}", userSignupDTO.Email);
                return ServiceResult<User>.Fail("Could not sign up user", ErrorType.ValidationError);
            }

            // Password is hashed and added to the user object below
            var result = await userManager.CreateAsync(user, userSignupDTO.Password);

            string errorMessage = "";

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    errorMessage = string.Join("\n", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                }
                logger.LogWarning("Failed signup attempt for {Email}, {ErrorMessage}", userSignupDTO.Email, errorMessage);
                return ServiceResult<User>.Fail("Could not sign up user", ErrorType.ValidationError);
            }
            await userManager.AddToRoleAsync(user, ApiRoles.User);
            return ServiceResult<User>.Created(user);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while signing up user with {Email}", userSignupDTO.Email);
            return ServiceResult<User>.Fail("Something went wrong while signing up.", ErrorType.InternalError);
        }
    }


    public async Task<ServiceResult<UserAuthResponseDTO>> Login(UserLoginDTO userLoginDTO)
    {
        try
        {
            var user = await userManager.FindByEmailAsync(userLoginDTO.Email);
            
            if (user == null)
            {
                logger.LogWarning("Failed login attempt for {Email}: User not found", userLoginDTO.Email);
                return ServiceResult<UserAuthResponseDTO>.Fail("Unable to log in", ErrorType.ValidationError);
            }

            bool passwordIsValid = await userManager.CheckPasswordAsync(user, userLoginDTO.Password);

            if (!passwordIsValid)
            {
                logger.LogWarning("Failed login attempt for {Email}: Invalid password", userLoginDTO.Email);
                return ServiceResult<UserAuthResponseDTO>.Fail("Unable to log in", ErrorType.ValidationError);
            }

            var tokenResult = await tokenService.GenerateToken(user);

            if (tokenResult.IsError)
            {
                logger.LogWarning("Failed to generate token for {Email}", user.Email);
                return ServiceResult<UserAuthResponseDTO>.Fail("Unable to generate token", ErrorType.InternalError);
            }

            var response = new UserAuthResponseDTO
            {
                Token = tokenResult.Payload,
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email
            };

            return ServiceResult<UserAuthResponseDTO>.Success(response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error while signing in user with {Email}", userLoginDTO.Email);
            return ServiceResult<UserAuthResponseDTO>.Fail("Something went wrong while signing in.", ErrorType.Unauthorized);
        }
    }
}