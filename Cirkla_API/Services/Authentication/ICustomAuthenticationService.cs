using Microsoft.AspNetCore.Mvc;
using Mapping.DTOs.Users;
using Cirkla_API.Common;
using Cirkla_DAL.Models;

namespace Cirkla_API.Services.Authentication;

public interface ICustomAuthenticationService
{
    Task<ServiceResult<User>> Signup(UserSignupDTO userSignupDTO);
    Task<ServiceResult<UserAuthResponseDTO>> Login(UserLoginDTO userLoginDTO);
}
