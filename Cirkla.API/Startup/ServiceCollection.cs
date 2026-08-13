using Cirkla.API.Middleware;
using Cirkla.API.Services.Authentication;
using Cirkla.API.Services.BorrowingContracts;
using Cirkla.API.Services.ItemPictures;
using Cirkla.API.Services.Items;
using Cirkla.API.Services.TimeLines;
using Cirkla.API.Services.TokenGenerator;
using Cirkla.Data;
using Cirkla.Data.Models;
using Cirkla.Data.Repositories.Contracts;
using Cirkla.Data.Repositories.ItemPictures;
using Cirkla.Data.Repositories.Items;
using Cirkla.Data.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using Cirkla.API.Backgroundservices.AutoArchive;
using Cirkla.API.Backgroundservices.AutoCancel;
using Cirkla.API.Backgroundservices.AutoLate;
using Cirkla.API.Services.CircleMembership;
using Cirkla.API.Services.Circles;
using Cirkla.API.Services.ContractNotifications;
using Cirkla.API.Services.Users;
using Cirkla.Data.Repositories.CircleJoinRequests;
using Cirkla.Data.Repositories.Circles;
using Cirkla.Data.Repositories.ContractNotifications;
using Cirkla.Data.Repositories.UoW;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Cirkla.Shared.Validators.Contracts;
using Microsoft.AspNetCore.Authorization;

namespace Cirkla.API.Startup;

public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAll(this IServiceCollection services)
        {


        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();

        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemService, ItemService>();

        services.AddScoped<IItemPictureRepository, ItemPictureRepository>();
        services.AddScoped<IItemPictureService, ItemPictureService>();

        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<ILoanTrackingService, LoanTrackingService>();
        services.AddScoped<IBorrowingContractService, BorrowingContractService>();

        services.AddScoped<IContractNotificationRepository, ContractNotificationRepository>();
        services.AddScoped<IContractNotificationService, ContractNotificationService>();

        services.AddScoped<ICircleRepository, CircleRepository>();
        services.AddScoped<ICircleService, CircleService>();

        services.AddScoped<ICircleJoinRequestRepository, CircleJoinRequestRepository>();
        services.AddScoped<ICircleMembershipService, CircleMembershipService>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddHostedService<AutoArchiveService>();
        services.AddHostedService<AutoLateService>();
        services.AddHostedService<AutoCancelService>();

        services.AddValidatorsFromAssemblyContaining<ContractCreateValidator>();

        services.AddSignalR().AddJsonProtocol(options =>
        {
            options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });

        // TODO: Remove this later, and their usings?
        //services.AddAuthorization(options =>
        //{
        //    options.AddPolicy("IsOwner", policy =>
        //        policy.Requirements.Add(new OwnershipRequirement()));
        //});

        //services.AddScoped<IAuthorizationHandler, OwnershipHandler>();


        // TODO: Safer CORS policy
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll",
                b => b.AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin());
        });

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });


        // TODO: Remove this later, for debugging
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.InvalidModelStateResponseFactory = context =>
            {
                var errors = context.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(e => e.Value.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                var logger = context.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
                logger.LogWarning("Model validation failed: {Errors}", string.Join("; ", errors));

                return new BadRequestObjectResult(new { Errors = errors });
            };
        });


        services.AddOpenApiDocument();
        services.AddEndpointsApiExplorer();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
        }
    }
