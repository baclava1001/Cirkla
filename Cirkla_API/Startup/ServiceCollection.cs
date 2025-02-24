﻿using Cirkla_API.Middleware;
using Cirkla_API.Services.BorrowingContracts;
using Cirkla_API.Services.ItemPictures;
using Cirkla_API.Services.Items;
using Cirkla_DAL;
using Cirkla_DAL.Models;
using Cirkla_DAL.Repositories.Contracts;
using Cirkla_DAL.Repositories.ItemPictures;
using Cirkla_DAL.Repositories.Items;
using Cirkla_DAL.Repositories.Users;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;
using Cirkla_API.Hubs.ContractUpdate;
using Cirkla_API.Services.Authentication;
using Cirkla_API.Services.TimeLines;
using Cirkla_API.Services.TokenGenerator;

namespace Cirkla_API.Startup;

public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterAll(this IServiceCollection services)
        {


        services.AddIdentityCore<User>()
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();

        services.AddScoped<IItemRepository, ItemRepository>();
        services.AddScoped<IItemService, ItemService>();

        services.AddScoped<IItemPictureRepository, ItemPictureRepository>();
        services.AddScoped<IItemPictureService, ItemPictureService>();

        services.AddScoped<IContractRepository, ContractRepository>();
        services.AddScoped<ITimeLineService, TimeLineService>();
        services.AddScoped<IBorrowingContractService, BorrowingContractService>();

        services.AddSignalR().AddJsonProtocol(options =>
        {
            options.PayloadSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });


        // Sends "heartbeat" updates to clients, testing purposes
        // services.AddHostedService<ServerTimeNotifier>(); 

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

        services.AddOpenApiDocument();
        services.AddEndpointsApiExplorer();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();

        return services;
        }
    }
