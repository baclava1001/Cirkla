using Cirkla_DAL.Models.Contract;
using Cirkla_DAL.Models.ItemPictures;
using Cirkla_DAL.Models.Items;
using Cirkla_DAL.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using static System.Net.WebRequestMethods;

namespace Cirkla_DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemPicture> ItemPictures { get; set; }
        public DbSet<Contract> Contracts { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Item>()
                .HasOne(i => i.Owner)
                .WithMany(o => o.Items)
                .HasForeignKey(i => i.OwnerId);

            builder.Entity<Item>()
                .HasMany(i => i.Pictures)
                .WithOne(p => p.Item);

            builder.Entity<Contract>()
                .HasOne(c => c.Owner)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Contract>()
                .HasOne(c => c.Borrower)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            //TODO: Extract seeding to a separate file

            builder.Entity<User>().HasData(GetUsers());
            builder.Entity<Item>().HasData(GetItems());
            builder.Entity<ItemPicture>().HasData(GetItemPictures());
        }


        #region Seed data
        private List<User> GetUsers()
        {
            PasswordHasher<User> passwordHasher = new();
            var users = new List<User>
            {
                new User
                {
                    Id = "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                    UserName = "samed.salman@gmail.com",
                    NormalizedUserName = "SAMED.SALMAN@GMAIL.COM",
                    FirstName = "Samed",
                    LastName = "Salman",
                    Address = "Hertx island",
                    ZipCode = "974 54",
                    Email = "samed.salman@gmail.com",
                    NormalizedEmail = "SAMED.SALMAN@GMAIL.COM",
                    PhoneNumber = "0737672491",
                    ProfilePictureURL = "https://avatar.iran.liara.run/public", // Generates random profile image
                    PasswordHash = passwordHasher.HashPassword(null, "Hejhopp@123"),
                    EmailConfirmed = true
                },
                new User
                {
                    Id = "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                    UserName = "kalle@kanin.se",
                    NormalizedUserName = "KALLE.KANIN.SE",
                    FirstName = "Kalle",
                    LastName = "Kanin",
                    Address = "Prärien",
                    ZipCode = "59784",
                    Email = "kalle@kanin.se",
                    NormalizedEmail = "KALLE.KANIN.SE",
                    PhoneNumber = "0920 555 888",
                    ProfilePictureURL = "https://avatar.iran.liara.run/public", // Generates random profile image
                    PasswordHash = passwordHasher.HashPassword(null ,"Hejhopp@123"),
                    EmailConfirmed = true
                },
                new User
                {
                    Id = "b2162ceb-793d-4e32-8029-ca56472dd93a",
                    UserName = "lizaminelli@popstar.com",
                    NormalizedUserName = "LIZAMINELLI@POPSTAR.COM",
                    FirstName = "Liza",
                    LastName = "Minelli",
                    Address = "Melrose Place",
                    ZipCode = "559412",
                    Email = "lizaminelli@popstar.com",
                    NormalizedEmail = "LIZAMINELLI@POPSTAR.COM",
                    PhoneNumber = "0920 252525",
                    ProfilePictureURL = "https://avatar.iran.liara.run/public", // Generates random profile image
                    PasswordHash = passwordHasher.HashPassword(null, "Hejhopp@123"),
                    EmailConfirmed = true
                }
            };
            return users;
        }

        public List<Item> GetItems()
        {
            var items = new List<Item>
            {
                new Item
                {
                    Id = 1,
                    Name = "Smartwatch",
                    Category = "Electronics",
                    Model = "Polar Grit X2 Pro Titanium Leather Bronze",
                    Specifications = "GPS + Cellular, 45mm",
                    Description = "Tracks fitness and health metrics.",
                    OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3"
                },
                new Item
                {
                    Id = 2,
                    Name = "High-end Laptop",
                    Category = "Electronics",
                    Model = "Dell XPS 15",
                    Specifications = "Intel i7, 16GB RAM, 512GB SSD",
                    Description = "Premium laptop for professionals.",
                    OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3"
                },
                new Item
                {
                    Id = 3,
                    Name = "Designer Bag",
                    Category = "Personal stuff",
                    Model = "Louis Vuitton Neverfull",
                    Specifications = "Monogram Canvas, Leather trim",
                    Description = "A timeless and spacious designer bag.",
                    OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3"
                },
                new Item
                {
                    Id = 4,
                    Name = "Noise Cancelling Headphones",
                    Category = "Electronics",
                    Model = "Bose 700",
                    Specifications = "Bluetooth, Noise Cancelling",
                    Description = "High-end headphones with superior noise cancellation.",
                    OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3"
                },
                new Item
                {
                    Id = 5,
                    Name = "Smartphone",
                    Category = "Electronics",
                    Model = "iPhone 13 Pro Max",
                    Specifications = "256GB, 6.7-inch display",
                    Description = "Top-tier smartphone with advanced features.",
                    OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4"
                },
                new Item
                {
                    Id = 6,
                    Name = "Luxury Watch",
                    Category = "Personal stuff",
                    Model = "Rolex Submariner",
                    Specifications = "Automatic, Stainless Steel",
                    Description = "Iconic luxury diving watch.",
                    OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4"
                },
                new Item
                {
                    Id = 7,
                    Name = "Electric Scooter",
                    Category = "Transportation",
                    Model = "Segway Ninebot Max",
                    Specifications = "350W Motor, 40 miles range",
                    Description = "High-performance electric scooter.",
                    OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4"
                },
                new Item
                {
                    Id = 8,
                    Name = "Camera",
                    Category = "Electronics",
                    Model = "Canon EOS R5",
                    Specifications = "45MP, 8K Video",
                    Description = "Professional-grade mirrorless camera.",
                    OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4"
                },
                new Item
                {
                    Id = 9,
                    Name = "Gaming Console",
                    Category = "Electronics",
                    Model = "PlayStation 5",
                    Specifications = "825GB SSD, 4K Gaming",
                    Description = "Next-gen gaming console.",
                    OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4"
                },
                new Item
                {
                    Id = 10,
                    Name = "Sound System",
                    Category = "Electronics",
                    Model = "Sonos Arc",
                    Specifications = "Dolby Atmos, Wi-Fi",
                    Description = "High-end soundbar for immersive audio.",
                    OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4"
                },
                new Item
                {
                    Id = 11,
                    Name = "4K TV",
                    Category = "Electronics",
                    Model = "Samsung QLED",
                    Specifications = "65-inch, 4K UHD",
                    Description = "State-of-the-art television with stunning picture quality.",
                    OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4"
                },
                new Item
                {
                    Id = 12,
                    Name = "Luxury Pen",
                    Category = "Personal stuff",
                    Model = "Montblanc Meisterstück",
                    Specifications = "Resin Barrel, Gold Trim",
                    Description = "Iconic fountain pen with exceptional craftsmanship.",
                    OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a"
                },
                new Item
                {
                    Id = 13,
                    Name = "Electric Car",
                    Category = "Transportation",
                    Model = "Tesla Model S",
                    Specifications = "Long Range, Autopilot",
                    Description = "Luxury electric car with advanced features.",
                    OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a"
                },
                new Item
                {
                    Id = 14,
                    Name = "Smart Home Speaker",
                    Category = "Electronics",
                    Model = "Google Nest Hub Max",
                    Specifications = "10-inch Display, Google Assistant",
                    Description = "Smart display with built-in assistant.",
                    OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a"
                },
                new Item
                {
                    Id = 15,
                    Name = "Air Purifier",
                    Category = "Houshold & tools",
                    Model = "Dyson Pure Cool",
                    Specifications = "HEPA Filter, Wi-Fi Enabled",
                    Description = "High-end air purifier for cleaner air.",
                    OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a"
                },
                new Item
                {
                    Id = 16,
                    Name = "Bluetooth Speaker",
                    Category = "Electronics",
                    Model = "Bang & Olufsen Beosound",
                    Specifications = "360-degree sound, Portable",
                    Description = "Premium portable speaker with excellent sound quality.",
                    OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a"
                },
                new Item
                {
                    Id = 17,
                    Name = "Drone",
                    Category = "Electronics",
                    Model = "DJI Mavic Air 2",
                    Specifications = "4K Camera, 34 min flight time",
                    Description = "High-performance drone for aerial photography.",
                    OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3"
                },
                new Item
                {
                    Id = 18,
                    Name = "VR Headset",
                    Category = "Electronics",
                    Model = "Oculus Quest 2",
                    Specifications = "128GB, All-in-One VR",
                    Description = "Immersive virtual reality experience.",
                    OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3"
                },
                new Item
                {
                    Id = 19,
                    Name = "Smart Thermostat",
                    Category = "Houshold & tools",
                    Model = "Nest Learning Thermostat",
                    Specifications = "Self-Learning, Wi-Fi",
                    Description = "Smart thermostat for energy-efficient home control.",
                    OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3"
                },
                new Item
                {
                    Id = 20,
                    Name = "High-End Coffee Maker",
                    Category = "Houshold & tools",
                    Model = "Breville Barista Express",
                    Specifications = "Espresso Machine, Built-in Grinder",
                    Description = "Professional-grade coffee maker for home baristas.",
                    OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4"
                }
            };

            return items;
        }

        private List<ItemPicture> GetItemPictures()
        {
            var itemPictures = new List<ItemPicture>
            {
                new ItemPicture
                {
                    Id = 1,
                    Url = "https://www.klockmagasinet.com/media/catalog/product/m/a/main_0.jpg?width=700&height=700&store=kk_se&image-type=image",
                    ItemId = 1
                },
                new ItemPicture
                {
                    Id = 2,
                    Url = "https://www.klockmagasinet.com/media/catalog/product/p/r/product_10_0.jpg?width=700&height=700&store=kk_se&image-type=image",
                    ItemId = 1
                },
                new ItemPicture
                {
                    Id = 3,
                    Url = "https://www.notebookcheck.se/uploads/tx_nbc2/DellXPS15-9510__1__02.jpg",
                    ItemId = 2
                },
                new ItemPicture
                {
                    Id = 4,
                    Url = "https://www.pcworld.com/wp-content/uploads/2024/04/dell-xps-15-2023-2.jpg?resize=1024%2C683&quality=50&strip=all",
                    ItemId = 2
                },
                new ItemPicture
                {
                    Id = 5,
                    Url = "https://www.careofcarl.se/bilder/artiklar/zoom/26143210_2.jpg?m=1702468978",
                    ItemId = 3
                },
                new ItemPicture
                {
                    Id = 6,
                    Url = "https://i.ebayimg.com/images/g/HLwAAOSwFEVmIFwF/s-l400.png",
                    ItemId = 3
                },
                new ItemPicture
                {
                    Id = 7,
                    Url = "https://img.tradera.net/large-fit/284/566851284_63fb4de8-6726-4f78-aed4-5e6fa4001cbe.jpg",
                    ItemId = 4
                },
                new ItemPicture
                {
                    Id = 8,
                    Url = "https://wp.inews.co.uk/wp-content/uploads/2021/09/PRI_200908332-760x570.jpg",
                    ItemId = 5
                },
                new ItemPicture
                {
                    Id = 9,
                    Url = "https://nymansur.commerce.services/product/raw/rolex-m126613lb-0002_2.png",
                    ItemId = 6
                },
                new ItemPicture
                {
                    Id = 10,
                    Url = "https://nymansur.commerce.services/product/raw/rolex-m126613lb-0002_3.png",
                    ItemId = 6
                },
                new ItemPicture
                {
                    Id = 11,
                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRaHm7uez_EIYmAIWKJ-CfENMtQPYmoPKHF5w&s",
                    ItemId = 11
                },
                new ItemPicture
                {
                    Id = 12,
                    Url = "https://listerhorsfall.co.uk/wp-content/uploads/2024/06/MB131344-7.jpg",
                    ItemId = 12
                },
                new ItemPicture
                {
                    Id = 13,
                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRoT9RUL5jsZ8G8HwhLIGMBtBBAeOcJphAcPg&s",
                    ItemId = 13
                },
                new ItemPicture
                {
                    Id = 14,
                    Url = "https://owp.klarna.com/product/640x640/3054959225/Google-Nest-Hub-Max.jpg?ph=true",
                    ItemId = 14
                },
                new ItemPicture
                {
                    Id = 15,
                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSnGtrrzm4FCTJW1eDrb9M1MJoH7JMV5yeiNQ&s",
                    ItemId = 15
                },
                new ItemPicture
                {
                    Id = 16,
                    Url = "https://the-gadgeteer.com/wp-content/uploads/2019/06/Dyson_Pure_Cool_16.jpg",
                    ItemId = 15
                },
                new ItemPicture
                {
                    Id = 17,
                    Url = "https://images.hifiklubben.com/image/e4cf3121-50ab-432d-8a35-b4a17ec3b7ee",
                    ItemId = 16
                },
                new ItemPicture
                {
                    Id = 18,
                    Url = "https://images.hifiklubben.com/image/bb9d43a0-2886-47b5-9b41-cd11322a16ea",
                    ItemId = 16
                },
                new ItemPicture
                {
                    Id = 19,
                    Url = "https://cdn.mos.cms.futurecdn.net/2mjes2QKryVCmU9dEReL6L.jpg",
                    ItemId = 17
                },
                new ItemPicture
                {
                    Id = 20,
                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMRuOgb22anrQFVd-1301SIV4fLRD_0sblbuGpudh1bj6-pd9c4I7u-t-q9K-U9dEcexc&usqp=CAU",
                    ItemId = 17
                },
                new ItemPicture
                {
                    Id = 21,
                    Url = "https://cdn.mos.cms.futurecdn.net/zzjJ4bNcLthVTd6pTamotH-1920-80.jpg.webp",
                    ItemId = 18
                },
                new ItemPicture
                {
                    Id = 22,
                    Url = "https://cdn.mos.cms.futurecdn.net/F4nXfc5jX5oVYUFNwudJa3-970-80.jpg.webp",
                    ItemId = 18
                },
                new ItemPicture
                {
                    Id = 23,
                    Url = "https://cdn.mos.cms.futurecdn.net/p26Dp34kLtLuWy52VV6xz3-970-80.jpg.webp",
                    ItemId = 18
                },
                new ItemPicture
                {
                    Id = 24,
                    Url = "https://www.intelligentabodes.co.uk/wp-content/uploads/2019/02/NEST-learning-thermostats-intelligent-abodes.jpg",
                    ItemId = 19
                },
                new ItemPicture
                {
                    Id = 25,
                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJjWQk4K16rCDWrXZCi9smr43_wb299Ke-FQ&s",
                    ItemId = 20
                },
                new ItemPicture
                {
                    Id = 26,
                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRPpAUvCna2bt1kS3tXBhgQ4GQDQMBv6gEfMlwS2Xyw1Xam1BGvzhGdt2BKpNCYQWGDwsU&usqp=CAU",
                    ItemId = 20
                },
                new ItemPicture
                {
                    Id = 27,
                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-IvVN7S0UFhWV6z-n4RBaZ1YxoPZikj3ODA&s",
                    ItemId = 7
                },
                new ItemPicture
                {
                    Id = 28,
                    Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQD4qVS5aq3WC6EzFq1Dd--ggCn-FHrnMeXIQ&s",
                    ItemId = 7
                },
                new ItemPicture
                {
                    Id = 29, Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTCQpV8waVQTS2y8XXgXJX4H0L6biHgv9BfUA&s", 
                    ItemId = 7 
                }, 
                new ItemPicture 
                { 
                    Id = 30,
                    Url = "https://i1.adis.ws/i/canon/eos-r5_martin_bissig_lifestyle_05_c629aad3c2154f34b3d7d7ba5a509196?$70-30-header-4by3-dt-jpg$",
                    ItemId = 8
                },
                new ItemPicture 
                { 
                    Id = 31,
                    Url = "https://i1.adis.ws/i/canon/eos-r5-c_lifestyle_47-pro_7e76ebaee6314ff9a06d7f00f59f0d1a9",
                    ItemId = 8 
                },
                new ItemPicture 
                { 
                    Id = 32, 
                    Url = "https://cdn.mos.cms.futurecdn.net/cfxJWdTTkVAUXFxsdvTy3n-320-80.png",
                    ItemId = 8
                }, 
                new ItemPicture 
                {
                    Id = 33,
                    Url = "https://cdn.mos.cms.futurecdn.net/xaMxSfTmuD8nbaVGprXwS4.jpg",
                    ItemId = 10
                },
                new ItemPicture
                {
                    Id = 34,
                    Url = "https://cdn.mos.cms.futurecdn.net/cfxJWdTTkVAUXFxsdvTy3n-320-80.png",
                    ItemId = 9
                },
                new ItemPicture
                {
                    Id = 35,
                    Url = "https://www.cnet.com/a/img/resize/bebef835df90640f9aa2e4a2f2a2699cf53a301f/hub/2020/10/26/b60bfe6f-3193-4381-b0d4-ac628cdcc565/img-1419.jpg?auto=webp&width=1200",
                    ItemId = 9
                }
            };
            return itemPictures;
        }

        #endregion Seed data
    }
}
