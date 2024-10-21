﻿// <auto-generated />
using System;
using Cirkla_DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241021140618_AddingContractsTableCorrect3")]
    partial class AddingContractsTableCorrect3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Cirkla_DAL.Models.Contract.Contract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("BorrowerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("Created")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("EndTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("StartTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.HasIndex("BorrowerId");

                    b.HasIndex("ItemId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Contracts");
                });

            modelBuilder.Entity("Cirkla_DAL.Models.ItemPictures.ItemPicture", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemPictures");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ItemId = 1,
                            Url = "https://www.klockmagasinet.com/media/catalog/product/m/a/main_0.jpg?width=700&height=700&store=kk_se&image-type=image"
                        },
                        new
                        {
                            Id = 2,
                            ItemId = 1,
                            Url = "https://www.klockmagasinet.com/media/catalog/product/p/r/product_10_0.jpg?width=700&height=700&store=kk_se&image-type=image"
                        },
                        new
                        {
                            Id = 3,
                            ItemId = 2,
                            Url = "https://www.notebookcheck.se/uploads/tx_nbc2/DellXPS15-9510__1__02.jpg"
                        },
                        new
                        {
                            Id = 4,
                            ItemId = 2,
                            Url = "https://www.pcworld.com/wp-content/uploads/2024/04/dell-xps-15-2023-2.jpg?resize=1024%2C683&quality=50&strip=all"
                        },
                        new
                        {
                            Id = 5,
                            ItemId = 3,
                            Url = "https://www.careofcarl.se/bilder/artiklar/zoom/26143210_2.jpg?m=1702468978"
                        },
                        new
                        {
                            Id = 6,
                            ItemId = 3,
                            Url = "https://i.ebayimg.com/images/g/HLwAAOSwFEVmIFwF/s-l400.png"
                        },
                        new
                        {
                            Id = 7,
                            ItemId = 4,
                            Url = "https://img.tradera.net/large-fit/284/566851284_63fb4de8-6726-4f78-aed4-5e6fa4001cbe.jpg"
                        },
                        new
                        {
                            Id = 8,
                            ItemId = 5,
                            Url = "https://wp.inews.co.uk/wp-content/uploads/2021/09/PRI_200908332-760x570.jpg"
                        },
                        new
                        {
                            Id = 9,
                            ItemId = 6,
                            Url = "https://nymansur.commerce.services/product/raw/rolex-m126613lb-0002_2.png"
                        },
                        new
                        {
                            Id = 10,
                            ItemId = 6,
                            Url = "https://nymansur.commerce.services/product/raw/rolex-m126613lb-0002_3.png"
                        },
                        new
                        {
                            Id = 11,
                            ItemId = 11,
                            Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRaHm7uez_EIYmAIWKJ-CfENMtQPYmoPKHF5w&s"
                        },
                        new
                        {
                            Id = 12,
                            ItemId = 12,
                            Url = "https://listerhorsfall.co.uk/wp-content/uploads/2024/06/MB131344-7.jpg"
                        },
                        new
                        {
                            Id = 13,
                            ItemId = 13,
                            Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRoT9RUL5jsZ8G8HwhLIGMBtBBAeOcJphAcPg&s"
                        },
                        new
                        {
                            Id = 14,
                            ItemId = 14,
                            Url = "https://owp.klarna.com/product/640x640/3054959225/Google-Nest-Hub-Max.jpg?ph=true"
                        },
                        new
                        {
                            Id = 15,
                            ItemId = 15,
                            Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSnGtrrzm4FCTJW1eDrb9M1MJoH7JMV5yeiNQ&s"
                        },
                        new
                        {
                            Id = 16,
                            ItemId = 15,
                            Url = "https://the-gadgeteer.com/wp-content/uploads/2019/06/Dyson_Pure_Cool_16.jpg"
                        },
                        new
                        {
                            Id = 17,
                            ItemId = 16,
                            Url = "https://images.hifiklubben.com/image/e4cf3121-50ab-432d-8a35-b4a17ec3b7ee"
                        },
                        new
                        {
                            Id = 18,
                            ItemId = 16,
                            Url = "https://images.hifiklubben.com/image/bb9d43a0-2886-47b5-9b41-cd11322a16ea"
                        },
                        new
                        {
                            Id = 19,
                            ItemId = 17,
                            Url = "https://cdn.mos.cms.futurecdn.net/2mjes2QKryVCmU9dEReL6L.jpg"
                        },
                        new
                        {
                            Id = 20,
                            ItemId = 17,
                            Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMRuOgb22anrQFVd-1301SIV4fLRD_0sblbuGpudh1bj6-pd9c4I7u-t-q9K-U9dEcexc&usqp=CAU"
                        },
                        new
                        {
                            Id = 21,
                            ItemId = 18,
                            Url = "https://cdn.mos.cms.futurecdn.net/zzjJ4bNcLthVTd6pTamotH-1920-80.jpg.webp"
                        },
                        new
                        {
                            Id = 22,
                            ItemId = 18,
                            Url = "https://cdn.mos.cms.futurecdn.net/F4nXfc5jX5oVYUFNwudJa3-970-80.jpg.webp"
                        },
                        new
                        {
                            Id = 23,
                            ItemId = 18,
                            Url = "https://cdn.mos.cms.futurecdn.net/p26Dp34kLtLuWy52VV6xz3-970-80.jpg.webp"
                        },
                        new
                        {
                            Id = 24,
                            ItemId = 19,
                            Url = "https://www.intelligentabodes.co.uk/wp-content/uploads/2019/02/NEST-learning-thermostats-intelligent-abodes.jpg"
                        },
                        new
                        {
                            Id = 25,
                            ItemId = 20,
                            Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJjWQk4K16rCDWrXZCi9smr43_wb299Ke-FQ&s"
                        },
                        new
                        {
                            Id = 26,
                            ItemId = 20,
                            Url = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRPpAUvCna2bt1kS3tXBhgQ4GQDQMBv6gEfMlwS2Xyw1Xam1BGvzhGdt2BKpNCYQWGDwsU&usqp=CAU"
                        });
                });

            modelBuilder.Entity("Cirkla_DAL.Models.Items.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Category")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OwnerId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Specifications")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Category = "Electronics",
                            Description = "Tracks fitness and health metrics.",
                            Model = "Polar Grit X2 Pro Titanium Leather Bronze",
                            Name = "Smartwatch",
                            OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                            Specifications = "GPS + Cellular, 45mm"
                        },
                        new
                        {
                            Id = 2,
                            Category = "Electronics",
                            Description = "Premium laptop for professionals.",
                            Model = "Dell XPS 15",
                            Name = "High-end Laptop",
                            OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                            Specifications = "Intel i7, 16GB RAM, 512GB SSD"
                        },
                        new
                        {
                            Id = 3,
                            Category = "Personal stuff",
                            Description = "A timeless and spacious designer bag.",
                            Model = "Louis Vuitton Neverfull",
                            Name = "Designer Bag",
                            OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                            Specifications = "Monogram Canvas, Leather trim"
                        },
                        new
                        {
                            Id = 4,
                            Category = "Electronics",
                            Description = "High-end headphones with superior noise cancellation.",
                            Model = "Bose 700",
                            Name = "Noise Cancelling Headphones",
                            OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                            Specifications = "Bluetooth, Noise Cancelling"
                        },
                        new
                        {
                            Id = 5,
                            Category = "Electronics",
                            Description = "Top-tier smartphone with advanced features.",
                            Model = "iPhone 13 Pro Max",
                            Name = "Smartphone",
                            OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                            Specifications = "256GB, 6.7-inch display"
                        },
                        new
                        {
                            Id = 6,
                            Category = "Personal stuff",
                            Description = "Iconic luxury diving watch.",
                            Model = "Rolex Submariner",
                            Name = "Luxury Watch",
                            OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                            Specifications = "Automatic, Stainless Steel"
                        },
                        new
                        {
                            Id = 7,
                            Category = "Transportation",
                            Description = "High-performance electric scooter.",
                            Model = "Segway Ninebot Max",
                            Name = "Electric Scooter",
                            OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                            Specifications = "350W Motor, 40 miles range"
                        },
                        new
                        {
                            Id = 8,
                            Category = "Electronics",
                            Description = "Professional-grade mirrorless camera.",
                            Model = "Canon EOS R5",
                            Name = "Camera",
                            OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                            Specifications = "45MP, 8K Video"
                        },
                        new
                        {
                            Id = 9,
                            Category = "Electronics",
                            Description = "Next-gen gaming console.",
                            Model = "PlayStation 5",
                            Name = "Gaming Console",
                            OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                            Specifications = "825GB SSD, 4K Gaming"
                        },
                        new
                        {
                            Id = 10,
                            Category = "Electronics",
                            Description = "High-end soundbar for immersive audio.",
                            Model = "Sonos Arc",
                            Name = "Sound System",
                            OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                            Specifications = "Dolby Atmos, Wi-Fi"
                        },
                        new
                        {
                            Id = 11,
                            Category = "Electronics",
                            Description = "State-of-the-art television with stunning picture quality.",
                            Model = "Samsung QLED",
                            Name = "4K TV",
                            OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                            Specifications = "65-inch, 4K UHD"
                        },
                        new
                        {
                            Id = 12,
                            Category = "Personal stuff",
                            Description = "Iconic fountain pen with exceptional craftsmanship.",
                            Model = "Montblanc Meisterstück",
                            Name = "Luxury Pen",
                            OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a",
                            Specifications = "Resin Barrel, Gold Trim"
                        },
                        new
                        {
                            Id = 13,
                            Category = "Transportation",
                            Description = "Luxury electric car with advanced features.",
                            Model = "Tesla Model S",
                            Name = "Electric Car",
                            OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a",
                            Specifications = "Long Range, Autopilot"
                        },
                        new
                        {
                            Id = 14,
                            Category = "Electronics",
                            Description = "Smart display with built-in assistant.",
                            Model = "Google Nest Hub Max",
                            Name = "Smart Home Speaker",
                            OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a",
                            Specifications = "10-inch Display, Google Assistant"
                        },
                        new
                        {
                            Id = 15,
                            Category = "Houshold & tools",
                            Description = "High-end air purifier for cleaner air.",
                            Model = "Dyson Pure Cool",
                            Name = "Air Purifier",
                            OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a",
                            Specifications = "HEPA Filter, Wi-Fi Enabled"
                        },
                        new
                        {
                            Id = 16,
                            Category = "Electronics",
                            Description = "Premium portable speaker with excellent sound quality.",
                            Model = "Bang & Olufsen Beosound",
                            Name = "Bluetooth Speaker",
                            OwnerId = "b2162ceb-793d-4e32-8029-ca56472dd93a",
                            Specifications = "360-degree sound, Portable"
                        },
                        new
                        {
                            Id = 17,
                            Category = "Electronics",
                            Description = "High-performance drone for aerial photography.",
                            Model = "DJI Mavic Air 2",
                            Name = "Drone",
                            OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                            Specifications = "4K Camera, 34 min flight time"
                        },
                        new
                        {
                            Id = 18,
                            Category = "Electronics",
                            Description = "Immersive virtual reality experience.",
                            Model = "Oculus Quest 2",
                            Name = "VR Headset",
                            OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                            Specifications = "128GB, All-in-One VR"
                        },
                        new
                        {
                            Id = 19,
                            Category = "Houshold & tools",
                            Description = "Smart thermostat for energy-efficient home control.",
                            Model = "Nest Learning Thermostat",
                            Name = "Smart Thermostat",
                            OwnerId = "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                            Specifications = "Self-Learning, Wi-Fi"
                        },
                        new
                        {
                            Id = 20,
                            Category = "Houshold & tools",
                            Description = "Professional-grade coffee maker for home baristas.",
                            Model = "Breville Barista Express",
                            Name = "High-End Coffee Maker",
                            OwnerId = "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                            Specifications = "Espresso Machine, Built-in Grinder"
                        });
                });

            modelBuilder.Entity("Cirkla_DAL.Models.Users.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfilePictureURL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ZipCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Cirkla_DAL.Models.Contract.Contract", b =>
                {
                    b.HasOne("Cirkla_DAL.Models.Users.User", "Borrower")
                        .WithMany()
                        .HasForeignKey("BorrowerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cirkla_DAL.Models.Items.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cirkla_DAL.Models.Users.User", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Borrower");

                    b.Navigation("Item");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Cirkla_DAL.Models.ItemPictures.ItemPicture", b =>
                {
                    b.HasOne("Cirkla_DAL.Models.Items.Item", "Item")
                        .WithMany("Pictures")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Cirkla_DAL.Models.Items.Item", b =>
                {
                    b.HasOne("Cirkla_DAL.Models.Users.User", "Owner")
                        .WithMany("Items")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Cirkla_DAL.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Cirkla_DAL.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Cirkla_DAL.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Cirkla_DAL.Models.Users.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Cirkla_DAL.Models.Items.Item", b =>
                {
                    b.Navigation("Pictures");
                });

            modelBuilder.Entity("Cirkla_DAL.Models.Users.User", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
