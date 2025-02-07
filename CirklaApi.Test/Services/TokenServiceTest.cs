using Cirkla_API.Services.TokenGenerator;
using Cirkla_DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.IdentityModel.Tokens.Jwt;
using Cirkla_API.Common.Constants;
using Test.CirklaApi.Common;
using Test.CirklaApi.MockDataGeneration;

namespace Test.CirklaApi.Services
{
    public class TokenServiceTest
    {
        private readonly Mock<ILogger<TokenService>> _loggerMock = new();
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<UserManager<User>> _userManagerMock = UserManagerMocker.Mock<User>();
        private readonly TokenService _tokenService;

        public TokenServiceTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _configurationMock.Setup(c => c["JwtSettings:Key"]).Returns("ThisIsASecretKey1234567890123459");
            _configurationMock.Setup(c => c["JwtSettings:Issuer"]).Returns("testIssuer");
            _configurationMock.Setup(c => c["JwtSettings:Audience"]).Returns("testAudience");
            _configurationMock.Setup(c => c["JwtSettings:DurationInMinutes"]).Returns("60"); // Must be a valid integer string

            _tokenService = new TokenService(
                _userManagerMock.Object,
                _configurationMock.Object,
                _loggerMock.Object
            );
        }

        [Fact]
        public async Task GenerateToken_ReturnsToken_WhenUserIsValid()
        {
            // Arrange
            var user = FakeDataGenerator.GenerateUser();

            // Act
            var result = await _tokenService.GenerateToken(user);

            // Assert
            Assert.NotNull(result.Payload);
            Assert.True(ValidJwtChecker.IsValid(result.Payload, user));
        }


        [Fact]
        public async Task GenerateToken_ReturnsError_WhenUserIsNull()
        {
            // Arrange
            User user = null;

            // Act
            var result = await _tokenService.GenerateToken(user);

            // Assert
            Assert.Equal(ErrorType.ValidationError, result.Error);
        }

        // TODO: Ask Copilot to write some more tests for this
    }
}
