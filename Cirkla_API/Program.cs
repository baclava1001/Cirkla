using Cirkla_API.Common.Constants;
using Cirkla_API.Startup;
using Cirkla_DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Cirkla_API.Hubs.ContractUpdate;

namespace Cirkla_API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Logging.AddConsole();
            
            // Create services to the container
            builder.Services.RegisterAll(); // Extension method in Startup/ServiceCollection.cs

            // Services that need configuring
            builder.Services.AddDbContext<AppDbContext>
                (options => options.UseSqlServer(builder.Configuration
                .GetConnectionString("AppConnectionString"))
                    .EnableSensitiveDataLogging());


            builder.Services.AddAuthorization();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                        ValidAudience = builder.Configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                    };
                });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseOpenApi();
                app.UseSwaggerUi();
            }


            // Seed data
            if (app.Environment.IsDevelopment())
            {
                using (var scope = app.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    var dbContext = services.GetRequiredService<AppDbContext>();
                    SeedData.Initialize(dbContext);
                }
            }


            app.UseHttpsRedirection();

            // TODO: Safer CORS policy
            app.UseCors("AllowAll");


            // TODO: Remove these and replace with antiforgery?
            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.MapHub<ContractUpdateHub>("contractNotifications");

            // Seed roles on startup
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { ApiRoles.User, ApiRoles.Administrator };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            app.Run();
        }
    }
}
