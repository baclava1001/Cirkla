using Cirkla_API.Common.Constants;
using Cirkla_API.Common;
using Cirkla_API.Services.Authentication;
using Cirkla_API.Services.TokenGenerator;
using Mapping.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Moq;
using Test.CirklaApi.MockDataGeneration;
using Microsoft.Extensions.Configuration;
using Cirkla_DAL.Models;
using Mapping.DTOs.Users;
using Test.CirklaApi.Common;

namespace Test.CirklaApi.Services;

public class CustomAuthenticationServiceTest
{
    private readonly Mock<ITokenService> _tokenServiceMock;
    private readonly Mock<IConfiguration> _configurationMock;
    private readonly Mock<ILogger<CustomAuthenticationService>> _loggerMock;
    private readonly Mock<UserManager<User>> _userManagerMock;
    private readonly CustomAuthenticationService _authenticationService;

    public CustomAuthenticationServiceTest()
    {
        _tokenServiceMock = new Mock<ITokenService>();
        _configurationMock = new Mock<IConfiguration>();
        _loggerMock = new Mock<ILogger<CustomAuthenticationService>>();
        _userManagerMock = UserManagerMocker.Mock<User>();
        _authenticationService = new CustomAuthenticationService(
            _userManagerMock.Object,
            _tokenServiceMock.Object,
            _configurationMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task Signup_ReturnsSuccess_WhenUserIsValid()
    {
        // Arrange
        var user = FakeDataGenerator.GenerateUser();
        var userSignupDTO = new UserSignupDTO
        {
            Address = user.Address,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.PasswordHash,
            ProfilePictureURL = user.ProfilePictureURL,
            ZipCode = user.ZipCode
        };

        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);

        // Act
        var result = await _authenticationService.Signup(userSignupDTO);

        // Assert
        Assert.False(result.IsError);
        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Signup_ReturnsFail_WhenUserCreationFails()
    {
        // Arrange
        var user = FakeDataGenerator.GenerateUser();
        var userSignupDTO = new UserSignupDTO
        {
            Address = user.Address,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Password = user.PasswordHash,
            ProfilePictureURL = user.ProfilePictureURL,
            ZipCode = user.ZipCode
        };

        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Failed(new IdentityError { Code = "Error", Description = "Error creating user" }));

        // Act
        var result = await _authenticationService.Signup(userSignupDTO);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Could not sign up user", result.ErrorMessage);
    }

    [Fact]
    public async Task Login_ReturnsSuccess_WhenUserIsValid()
    {
        // Arrange
        var user = FakeDataGenerator.GenerateUser();
        var userLoginDTO = new UserLoginDTO
        {
            Email = user.Email,
            Password = user.PasswordHash
        };
        var tokenResult = ServiceResult<string>.Success("valid_token");

        _userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
        _tokenServiceMock.Setup(ts => ts.GenerateToken(It.IsAny<User>())).ReturnsAsync(tokenResult);

        // Act
        var result = await _authenticationService.Login(userLoginDTO);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal("valid_token", result.Payload.Token);
        _userManagerMock.Verify(um => um.FindByEmailAsync(It.IsAny<string>()), Times.Once);
        _tokenServiceMock.Verify(ts => ts.GenerateToken(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task Login_ReturnsFail_WhenUserNotFound()
    {
        // Arrange
        var user = FakeDataGenerator.GenerateUser();
        var userLoginDTO = new UserLoginDTO
        {
            Email = user.Email,
            Password = user.PasswordHash
        };

        _userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((User)null);

        // Act
        var result = await _authenticationService.Login(userLoginDTO);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Unable to log in", result.ErrorMessage);
        _userManagerMock.Verify(um => um.FindByEmailAsync(It.IsAny<string>()), Times.Once);
    }

    [Fact]
    public async Task Login_ReturnsFail_WhenPasswordIsIncorrect()
    {
        // Arrange
        var user = FakeDataGenerator.GenerateUser();
        var userLoginDTO = new UserLoginDTO
        {
            Email = user.Email,
            Password = user.PasswordHash
        };
        _userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(false);

        // Act
        var result = await _authenticationService.Login(userLoginDTO);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Unable to log in", result.ErrorMessage);
    }

    [Fact]
    public async Task Login_ReturnsFail_WhenTokenGenerationFails()
    {
        // Arrange
        var user = FakeDataGenerator.GenerateUser();
        var userLoginDTO = new UserLoginDTO
        {
            Email = user.Email,
            Password = user.PasswordHash
        };
        var tokenResult = ServiceResult<string>.Fail("Failed to generate token", ErrorType.InternalError);

        _userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(um => um.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>())).ReturnsAsync(true);
        _tokenServiceMock.Setup(ts => ts.GenerateToken(It.IsAny<User>())).ReturnsAsync(tokenResult);

        // Act
        var result = await _authenticationService.Login(userLoginDTO);

        // Assert
        Assert.True(result.IsError);
        Assert.Equal("Unable to generate token", result.ErrorMessage);
    }
}
