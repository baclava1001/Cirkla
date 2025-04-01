using Cirkla_API.Common;
using Cirkla_API.Common.Constants;
using Cirkla_API.Controllers;
using Cirkla_API.Services.TokenGenerator;
using Cirkla_DAL.Models;
using Mapping.DTOs.Users;
using Mapping.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Services.Authentication;

public class CustomAuthenticationService : ICustomAuthenticationService
{
    private readonly UserManager<User> _userManager;
    private readonly ITokenService _tokenService;
    private readonly IConfiguration _configuration;
    private readonly ILogger<CustomAuthenticationService> _logger;
    
    public CustomAuthenticationService(
        UserManager<User> userManager,
        ITokenService tokenService,
        IConfiguration configuration,
        ILogger<CustomAuthenticationService> logger)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<ServiceResult<User>> Signup(UserSignupDTO userSignupDTO)
    {
        try
        {
            User user = await Mapper.MapToUser(userSignupDTO);
            if (user == null)
            {
                _logger.LogError("Failed to map UserSignupDTO to User for {Email}", userSignupDTO.Email);
                return ServiceResult<User>.Fail("Could not sign up user", ErrorType.ValidationError);
            }

            // Password is hashed and added to the user object below
            var result = await _userManager.CreateAsync(user, userSignupDTO.Password);

            string errorMessage = "";

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    errorMessage = string.Join("\n", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                }
                _logger.LogWarning("Failed signup attempt for {Email}, {ErrorMessage}", userSignupDTO.Email, errorMessage);
                return ServiceResult<User>.Fail("Could not sign up user", ErrorType.ValidationError);
            }
            await _userManager.AddToRoleAsync(user, ApiRoles.User);
            return ServiceResult<User>.Created(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while signing up user with {Email}", userSignupDTO.Email);
            return ServiceResult<User>.Fail("Something went wrong while signing up.", ErrorType.InternalError);
        }
    }



    public async Task<ServiceResult<UserAuthResponseDTO>> Login(UserLoginDTO userLoginDTO)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(userLoginDTO.Email);
            
            if (user == null)
            {
                _logger.LogWarning("Failed login attempt for {Email}: User not found", userLoginDTO.Email);
                return ServiceResult<UserAuthResponseDTO>.Fail("Unable to log in", ErrorType.ValidationError);
            }

            bool passwordIsValid = await _userManager.CheckPasswordAsync(user, userLoginDTO.Password);

            if (!passwordIsValid)
            {
                _logger.LogWarning("Failed login attempt for {Email}: Invalid password", userLoginDTO.Email);
                return ServiceResult<UserAuthResponseDTO>.Fail("Unable to log in", ErrorType.ValidationError);
            }

            var tokenResult = await _tokenService.GenerateToken(user);

            if (tokenResult.IsError)
            {
                _logger.LogWarning("Failed to generate token for {Email}", user.Email);
                return ServiceResult<UserAuthResponseDTO>.Fail("Unable to generate token", ErrorType.InternalError);
            }

            var response = new UserAuthResponseDTO
            {
                Email = user.Email,
                Token = tokenResult.Payload,
                Id = user.Id
            };

            return ServiceResult<UserAuthResponseDTO>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while signing in user with {Email}", userLoginDTO.Email);
            return ServiceResult<UserAuthResponseDTO>.Fail("Something went wrong while signing in.", ErrorType.Unauthorized);
        }
    }
}