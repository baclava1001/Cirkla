using Cirkla_DAL.Models.ItemPictures;
using Cirkla_DAL.Models.Items;
using Cirkla_DAL.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Cirkla_DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemPicture> ItemPictures { get; set; }

        // TODO: !!!Seed some data

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>().HasData(GetUsers());
            builder.Entity<Item>().HasData(GetItems());
            builder.Entity<ItemPicture>().HasData(GetItemPictures());
        }


        #region Seed data
        private List<User> GetUsers()
        {
            return Enumerable.Range(1, 10)
                .Select(index => new User
                {
                    Id = index,
                    FirstName = $"User{index}",
                    LastName = $"UserSon{index}",
                    Address = $"Melrose Place {index}",
                    ZipCode = $"{index}{index}{index}{index}{index}",
                    Email = $"User{index}.UserSon{index}@mail.com",
                    PhoneNumber = $"0920 {index}{index}{index} {index}{index}{index}",
                    ProfilePictureURL = "https://avatar.iran.liara.run/public" // Generates random profile image
                }).ToList();
        }

        private List<Item> GetItems()
        {
            Random randomUser = new Random();
            return Enumerable.Range(1, 30)
                .Select(index => new Item
                {
                    Id = index,
                    Name = $"{index}st thingamagig",
                    Category = $"General stuff",
                    Model = "gobbledygook",
                    Specifications = "",
                    Description = "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.",
                    OwnerId = randomUser.Next(1, 10)
                }).ToList();

        }

        private List<ItemPicture> GetItemPictures()
        {
            Random randomItem = new Random();
            return Enumerable.Range(1, 90)
                .Select(index => new ItemPicture
                {
                    Id = index,
                    Url = "https://loremflickr.com/420/420/tool", // Generates random placeholder image
                    ItemId = randomItem.Next(1, 30)
                }).ToList();
        }

        #endregion Seed data
    }
}
