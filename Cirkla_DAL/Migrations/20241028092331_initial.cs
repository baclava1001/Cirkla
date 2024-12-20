﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Contracts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    OwnerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BorrowerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contracts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contracts_AspNetUsers_BorrowerId",
                        column: x => x.BorrowerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_AspNetUsers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contracts_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemPictures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPictures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ItemPictures_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Address", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "ProfilePictureURL", "SecurityStamp", "TwoFactorEnabled", "UserName", "ZipCode" },
                values: new object[,]
                {
                    { "54b5627b-1f8e-4634-8bb0-206fecc840f3", 0, "Hertx island", "1149f207-1f12-4aa6-aea0-480bbcc55af8", "samed.salman@gmail.com", true, "Samed", "Salman", false, null, "SAMED.SALMAN@GMAIL.COM", "SAMED.SALMAN@GMAIL.COM", "AQAAAAIAAYagAAAAENpMP8xXTItwLpkj4383blGSkLoGNfVi9Ja3pFd01exSLIBcbphYATMyA1yKdFYkLQ==", "0737672491", false, "https://avatar.iran.liara.run/public", "c057d159-cbc6-4a34-a161-17df6b88a50b", false, "samed.salman@gmail.com", "974 54" },
                    { "6ce14244-d9f8-417e-b05f-df87f2c044e4", 0, "Prärien", "4457290b-9de8-4c26-a010-50ae3533c289", "kalle@kanin.se", true, "Kalle", "Kanin", false, null, "KALLE.KANIN.SE", "KALLE.KANIN.SE", "AQAAAAIAAYagAAAAELNaoyH6b5xipCTWCaobWVUZoZuoPVwDs63A3LquQ4jC3BteQMjBGxrZBublSZ52kg==", "0920 555 888", false, "https://avatar.iran.liara.run/public", "c2019ccf-c375-4663-a81e-3ff8a01e3b93", false, "kalle@kanin.se", "59784" },
                    { "b2162ceb-793d-4e32-8029-ca56472dd93a", 0, "Melrose Place", "97b77eaf-cbb8-4930-9445-3d69f3d53e2f", "lizaminelli@popstar.com", true, "Liza", "Minelli", false, null, "LIZAMINELLI@POPSTAR.COM", "LIZAMINELLI@POPSTAR.COM", "AQAAAAIAAYagAAAAEHLuloBhHGjbIlpihwIz7F2UIXFcYU17HcpnuZ1ElZ7fD840MeWeF+VgF74dmjw3jw==", "0920 252525", false, "https://avatar.iran.liara.run/public", "d5ecd196-ed06-4806-8536-2f7de14528ca", false, "lizaminelli@popstar.com", "559412" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Description", "Model", "Name", "OwnerId", "Specifications" },
                values: new object[,]
                {
                    { 1, "Electronics", "Tracks fitness and health metrics.", "Polar Grit X2 Pro Titanium Leather Bronze", "Smartwatch", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "GPS + Cellular, 45mm" },
                    { 2, "Electronics", "Premium laptop for professionals.", "Dell XPS 15", "High-end Laptop", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "Intel i7, 16GB RAM, 512GB SSD" },
                    { 3, "Personal stuff", "A timeless and spacious designer bag.", "Louis Vuitton Neverfull", "Designer Bag", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "Monogram Canvas, Leather trim" },
                    { 4, "Electronics", "High-end headphones with superior noise cancellation.", "Bose 700", "Noise Cancelling Headphones", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "Bluetooth, Noise Cancelling" },
                    { 5, "Electronics", "Top-tier smartphone with advanced features.", "iPhone 13 Pro Max", "Smartphone", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "256GB, 6.7-inch display" },
                    { 6, "Personal stuff", "Iconic luxury diving watch.", "Rolex Submariner", "Luxury Watch", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "Automatic, Stainless Steel" },
                    { 7, "Transportation", "High-performance electric scooter.", "Segway Ninebot Max", "Electric Scooter", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "350W Motor, 40 miles range" },
                    { 8, "Electronics", "Professional-grade mirrorless camera.", "Canon EOS R5", "Camera", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "45MP, 8K Video" },
                    { 9, "Electronics", "Next-gen gaming console.", "PlayStation 5", "Gaming Console", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "825GB SSD, 4K Gaming" },
                    { 10, "Electronics", "High-end soundbar for immersive audio.", "Sonos Arc", "Sound System", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "Dolby Atmos, Wi-Fi" },
                    { 11, "Electronics", "State-of-the-art television with stunning picture quality.", "Samsung QLED", "4K TV", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "65-inch, 4K UHD" },
                    { 12, "Personal stuff", "Iconic fountain pen with exceptional craftsmanship.", "Montblanc Meisterstück", "Luxury Pen", "b2162ceb-793d-4e32-8029-ca56472dd93a", "Resin Barrel, Gold Trim" },
                    { 13, "Transportation", "Luxury electric car with advanced features.", "Tesla Model S", "Electric Car", "b2162ceb-793d-4e32-8029-ca56472dd93a", "Long Range, Autopilot" },
                    { 14, "Electronics", "Smart display with built-in assistant.", "Google Nest Hub Max", "Smart Home Speaker", "b2162ceb-793d-4e32-8029-ca56472dd93a", "10-inch Display, Google Assistant" },
                    { 15, "Houshold & tools", "High-end air purifier for cleaner air.", "Dyson Pure Cool", "Air Purifier", "b2162ceb-793d-4e32-8029-ca56472dd93a", "HEPA Filter, Wi-Fi Enabled" },
                    { 16, "Electronics", "Premium portable speaker with excellent sound quality.", "Bang & Olufsen Beosound", "Bluetooth Speaker", "b2162ceb-793d-4e32-8029-ca56472dd93a", "360-degree sound, Portable" },
                    { 17, "Electronics", "High-performance drone for aerial photography.", "DJI Mavic Air 2", "Drone", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "4K Camera, 34 min flight time" },
                    { 18, "Electronics", "Immersive virtual reality experience.", "Oculus Quest 2", "VR Headset", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "128GB, All-in-One VR" },
                    { 19, "Houshold & tools", "Smart thermostat for energy-efficient home control.", "Nest Learning Thermostat", "Smart Thermostat", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "Self-Learning, Wi-Fi" },
                    { 20, "Houshold & tools", "Professional-grade coffee maker for home baristas.", "Breville Barista Express", "High-End Coffee Maker", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "Espresso Machine, Built-in Grinder" }
                });

            migrationBuilder.InsertData(
                table: "ItemPictures",
                columns: new[] { "Id", "ItemId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "https://www.klockmagasinet.com/media/catalog/product/m/a/main_0.jpg?width=700&height=700&store=kk_se&image-type=image" },
                    { 2, 1, "https://www.klockmagasinet.com/media/catalog/product/p/r/product_10_0.jpg?width=700&height=700&store=kk_se&image-type=image" },
                    { 3, 2, "https://www.notebookcheck.se/uploads/tx_nbc2/DellXPS15-9510__1__02.jpg" },
                    { 4, 2, "https://www.pcworld.com/wp-content/uploads/2024/04/dell-xps-15-2023-2.jpg?resize=1024%2C683&quality=50&strip=all" },
                    { 5, 3, "https://www.careofcarl.se/bilder/artiklar/zoom/26143210_2.jpg?m=1702468978" },
                    { 6, 3, "https://i.ebayimg.com/images/g/HLwAAOSwFEVmIFwF/s-l400.png" },
                    { 7, 4, "https://img.tradera.net/large-fit/284/566851284_63fb4de8-6726-4f78-aed4-5e6fa4001cbe.jpg" },
                    { 8, 5, "https://wp.inews.co.uk/wp-content/uploads/2021/09/PRI_200908332-760x570.jpg" },
                    { 9, 6, "https://nymansur.commerce.services/product/raw/rolex-m126613lb-0002_2.png" },
                    { 10, 6, "https://nymansur.commerce.services/product/raw/rolex-m126613lb-0002_3.png" },
                    { 11, 11, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRaHm7uez_EIYmAIWKJ-CfENMtQPYmoPKHF5w&s" },
                    { 12, 12, "https://listerhorsfall.co.uk/wp-content/uploads/2024/06/MB131344-7.jpg" },
                    { 13, 13, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRoT9RUL5jsZ8G8HwhLIGMBtBBAeOcJphAcPg&s" },
                    { 14, 14, "https://owp.klarna.com/product/640x640/3054959225/Google-Nest-Hub-Max.jpg?ph=true" },
                    { 15, 15, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSnGtrrzm4FCTJW1eDrb9M1MJoH7JMV5yeiNQ&s" },
                    { 16, 15, "https://the-gadgeteer.com/wp-content/uploads/2019/06/Dyson_Pure_Cool_16.jpg" },
                    { 17, 16, "https://images.hifiklubben.com/image/e4cf3121-50ab-432d-8a35-b4a17ec3b7ee" },
                    { 18, 16, "https://images.hifiklubben.com/image/bb9d43a0-2886-47b5-9b41-cd11322a16ea" },
                    { 19, 17, "https://cdn.mos.cms.futurecdn.net/2mjes2QKryVCmU9dEReL6L.jpg" },
                    { 20, 17, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMRuOgb22anrQFVd-1301SIV4fLRD_0sblbuGpudh1bj6-pd9c4I7u-t-q9K-U9dEcexc&usqp=CAU" },
                    { 21, 18, "https://cdn.mos.cms.futurecdn.net/zzjJ4bNcLthVTd6pTamotH-1920-80.jpg.webp" },
                    { 22, 18, "https://cdn.mos.cms.futurecdn.net/F4nXfc5jX5oVYUFNwudJa3-970-80.jpg.webp" },
                    { 23, 18, "https://cdn.mos.cms.futurecdn.net/p26Dp34kLtLuWy52VV6xz3-970-80.jpg.webp" },
                    { 24, 19, "https://www.intelligentabodes.co.uk/wp-content/uploads/2019/02/NEST-learning-thermostats-intelligent-abodes.jpg" },
                    { 25, 20, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJjWQk4K16rCDWrXZCi9smr43_wb299Ke-FQ&s" },
                    { 26, 20, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRPpAUvCna2bt1kS3tXBhgQ4GQDQMBv6gEfMlwS2Xyw1Xam1BGvzhGdt2BKpNCYQWGDwsU&usqp=CAU" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_BorrowerId",
                table: "Contracts",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_ItemId",
                table: "Contracts",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Contracts_OwnerId",
                table: "Contracts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemPictures_ItemId",
                table: "ItemPictures",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OwnerId",
                table: "Items",
                column: "OwnerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Contracts");

            migrationBuilder.DropTable(
                name: "ItemPictures");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
