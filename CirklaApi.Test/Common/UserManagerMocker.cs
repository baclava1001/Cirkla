using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Cirkla_DAL.Models;

namespace Test.CirklaApi.Common;

public static class UserManagerMocker
{
    public static Mock<UserManager<TUser>> Mock<TUser>() where TUser : class
    {
        var store = new Mock<IUserStore<TUser>>();
        var options = new Mock<IOptions<IdentityOptions>>();
        var passwordHasher = new Mock<IPasswordHasher<TUser>>();
        var userValidators = new List<IUserValidator<TUser>> { new Mock<IUserValidator<TUser>>().Object };
        var passwordValidators = new List<IPasswordValidator<TUser>> { new Mock<IPasswordValidator<TUser>>().Object };
        var lookupNormalizer = new Mock<ILookupNormalizer>();
        var identityErrorDescriber = new Mock<IdentityErrorDescriber>();
        var services = new Mock<IServiceProvider>();
        var logger = new Mock<ILogger<UserManager<TUser>>>();

        var userManagerMock = new Mock<UserManager<TUser>>(
            store.Object, options.Object, passwordHasher.Object,
            userValidators, passwordValidators, lookupNormalizer.Object,
            identityErrorDescriber.Object, services.Object, logger.Object
        );

        userManagerMock
            .Setup(um => um.GetRolesAsync(It.IsAny<TUser>()))
            .ReturnsAsync(new List<string> { "Admin", "User" });

        userManagerMock
            .Setup(um => um.GetClaimsAsync(It.IsAny<TUser>()))
            .ReturnsAsync(new List<Claim> { new Claim("CustomClaim", "Value") });


        return userManagerMock;
    }
}