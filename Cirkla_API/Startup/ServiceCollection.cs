using Cirkla_API.Middleware;
using Cirkla_API.Services.Authentication;
using Cirkla_API.Services.BorrowingContracts;
using Cirkla_API.Services.ItemPictures;
using Cirkla_API.Services.Items;
using Cirkla_API.Services.TimeLines;
using Cirkla_API.Services.TokenGenerator;
using Cirkla_DAL;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.ItemPictures;
using Cirkla_DAL.Repositories.Items;
using Cirkla_DAL.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using Cirkla_API.Backgroundservices.AutoArchive;
using Cirkla_API.Backgroundservices.AutoCancel;
using Cirkla_API.Backgroundservices.AutoLate;
using Cirkla_API.Services.CircleMembership;
using Cirkla_API.Services.CircleRequests;
using Cirkla_API.Services.Circles;
using Cirkla_API.Services.ContractNotifications;
using Cirkla_API.Services.Users;
using Cirkla_DAL.Repositories.CircleJoinRequests;
using Cirkla_DAL.Repositories.Circles;
using Cirkla_DAL.Repositories.ContractNotifications;
using Microsoft.AspNetCore.Mvc;

namespace Cirkla_API.Startup;

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
        services.AddScoped<ITimeLineService, TimeLineService>();
        services.AddScoped<IBorrowingContractService, BorrowingContractService>();

        services.AddScoped<IContractNotificationRepository, ContractNotificationRepository>();
        services.AddScoped<IContractNotificationService, ContractNotificationService>();

        services.AddScoped<ICircleRepository, CircleRepository>();
        services.AddScoped<ICircleService, CircleService>();

        services.AddScoped<ICircleJoinRequestRepository, CircleJoinRequestRepository>();
        services.AddScoped<ICircleMembershipService, CircleMembershipService>();

        services.AddHostedService<AutoArchiveService>();
        services.AddHostedService<AutoLateService>();
        services.AddHostedService<AutoCancelService>();

        services.AddSignalR().AddJsonProtocol(options =>
        {
            options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });

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
