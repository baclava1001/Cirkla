using Cirkla_API.ItemPictures;
using Cirkla_API.Items;
using Cirkla_API.Users;
using Cirkla_DAL;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // TODO: Try using Service decoration to contain services: https://dev.to/giannoudis/service-registration-and-decoration-in-aspnet-core-379d

            builder.Services.AddDbContext<AppDbContext>
                (options => options.UseSqlServer(builder.Configuration
                .GetConnectionString("AppConnectionString")));
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IItemRepository, ItemRepository>();
            builder.Services.AddScoped<IItemPictureRepository, ItemPictureRepository>();
            builder.Services.AddScoped<IProfileService, ProfileService>();
            builder.Services.AddScoped<IItemService, ItemService>();
            builder.Services.AddScoped<IItemPictureService, ItemPictureService>();
            
            // TODO: Add iLogger configuration

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
