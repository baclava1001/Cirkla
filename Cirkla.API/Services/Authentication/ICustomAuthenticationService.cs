using Microsoft.AspNetCore.Mvc;
using Cirkla.Shared.DTOs.Users;
using Cirkla.API.Common;
using Cirkla.Data.Models;

namespace Cirkla.API.Services.Authentication;

public interface ICustomAuthenticationService
{
    Task<ServiceResult<User>> Signup(UserSignupDTO userSignupDTO);
    Task<ServiceResult<UserAuthResponseDTO>> Login(UserLoginDTO userLoginDTO);
}
