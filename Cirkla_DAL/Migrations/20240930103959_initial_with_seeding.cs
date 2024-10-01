using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial_with_seeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProfilePictureURL = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Users_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Users",
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
                table: "Users",
                columns: new[] { "Id", "Address", "Email", "FirstName", "LastName", "PhoneNumber", "ProfilePictureURL", "ZipCode" },
                values: new object[,]
                {
                    { 1, "Melrose Place 1", "User1.UserSon1@mail.com", "User1", "UserSon1", "0920 111 111", "https://avatar.iran.liara.run/public", "11111" },
                    { 2, "Melrose Place 2", "User2.UserSon2@mail.com", "User2", "UserSon2", "0920 222 222", "https://avatar.iran.liara.run/public", "22222" },
                    { 3, "Melrose Place 3", "User3.UserSon3@mail.com", "User3", "UserSon3", "0920 333 333", "https://avatar.iran.liara.run/public", "33333" },
                    { 4, "Melrose Place 4", "User4.UserSon4@mail.com", "User4", "UserSon4", "0920 444 444", "https://avatar.iran.liara.run/public", "44444" },
                    { 5, "Melrose Place 5", "User5.UserSon5@mail.com", "User5", "UserSon5", "0920 555 555", "https://avatar.iran.liara.run/public", "55555" },
                    { 6, "Melrose Place 6", "User6.UserSon6@mail.com", "User6", "UserSon6", "0920 666 666", "https://avatar.iran.liara.run/public", "66666" },
                    { 7, "Melrose Place 7", "User7.UserSon7@mail.com", "User7", "UserSon7", "0920 777 777", "https://avatar.iran.liara.run/public", "77777" },
                    { 8, "Melrose Place 8", "User8.UserSon8@mail.com", "User8", "UserSon8", "0920 888 888", "https://avatar.iran.liara.run/public", "88888" },
                    { 9, "Melrose Place 9", "User9.UserSon9@mail.com", "User9", "UserSon9", "0920 999 999", "https://avatar.iran.liara.run/public", "99999" },
                    { 10, "Melrose Place 10", "User10.UserSon10@mail.com", "User10", "UserSon10", "0920 101010 101010", "https://avatar.iran.liara.run/public", "1010101010" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Description", "Model", "Name", "OwnerId", "Specifications" },
                values: new object[,]
                {
                    { 1, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "1st thingamagig", 7, "" },
                    { 2, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "2st thingamagig", 2, "" },
                    { 3, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "3st thingamagig", 6, "" },
                    { 4, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "4st thingamagig", 3, "" },
                    { 5, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "5st thingamagig", 1, "" },
                    { 6, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "6st thingamagig", 8, "" },
                    { 7, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "7st thingamagig", 9, "" },
                    { 8, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "8st thingamagig", 5, "" },
                    { 9, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "9st thingamagig", 4, "" },
                    { 10, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "10st thingamagig", 7, "" },
                    { 11, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "11st thingamagig", 1, "" },
                    { 12, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "12st thingamagig", 6, "" },
                    { 13, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "13st thingamagig", 3, "" },
                    { 14, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "14st thingamagig", 8, "" },
                    { 15, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "15st thingamagig", 1, "" },
                    { 16, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "16st thingamagig", 3, "" },
                    { 17, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "17st thingamagig", 9, "" },
                    { 18, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "18st thingamagig", 3, "" },
                    { 19, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "19st thingamagig", 6, "" },
                    { 20, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "20st thingamagig", 3, "" },
                    { 21, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "21st thingamagig", 4, "" },
                    { 22, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "22st thingamagig", 7, "" },
                    { 23, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "23st thingamagig", 8, "" },
                    { 24, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "24st thingamagig", 3, "" },
                    { 25, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "25st thingamagig", 4, "" },
                    { 26, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "26st thingamagig", 4, "" },
                    { 27, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "27st thingamagig", 1, "" },
                    { 28, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "28st thingamagig", 5, "" },
                    { 29, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "29st thingamagig", 6, "" },
                    { 30, "General stuff", "To be, or to takes, when he himself might his quietus makes us rather bear those ills we have, the native hue of resolution is sicklied o'er with the dreams may come when he himself might his quietus make with and their currents turn awry, and the slings and moment with this mortal coil, must give us pause.", "gobbledygook", "30st thingamagig", 2, "" }
                });

            migrationBuilder.InsertData(
                table: "ItemPictures",
                columns: new[] { "Id", "ItemId", "Url" },
                values: new object[,]
                {
                    { 1, 25, "https://loremflickr.com/420/420/tool" },
                    { 2, 9, "https://loremflickr.com/420/420/tool" },
                    { 3, 29, "https://loremflickr.com/420/420/tool" },
                    { 4, 18, "https://loremflickr.com/420/420/tool" },
                    { 5, 29, "https://loremflickr.com/420/420/tool" },
                    { 6, 13, "https://loremflickr.com/420/420/tool" },
                    { 7, 12, "https://loremflickr.com/420/420/tool" },
                    { 8, 3, "https://loremflickr.com/420/420/tool" },
                    { 9, 15, "https://loremflickr.com/420/420/tool" },
                    { 10, 9, "https://loremflickr.com/420/420/tool" },
                    { 11, 10, "https://loremflickr.com/420/420/tool" },
                    { 12, 29, "https://loremflickr.com/420/420/tool" },
                    { 13, 14, "https://loremflickr.com/420/420/tool" },
                    { 14, 6, "https://loremflickr.com/420/420/tool" },
                    { 15, 12, "https://loremflickr.com/420/420/tool" },
                    { 16, 18, "https://loremflickr.com/420/420/tool" },
                    { 17, 28, "https://loremflickr.com/420/420/tool" },
                    { 18, 22, "https://loremflickr.com/420/420/tool" },
                    { 19, 29, "https://loremflickr.com/420/420/tool" },
                    { 20, 13, "https://loremflickr.com/420/420/tool" },
                    { 21, 3, "https://loremflickr.com/420/420/tool" },
                    { 22, 14, "https://loremflickr.com/420/420/tool" },
                    { 23, 15, "https://loremflickr.com/420/420/tool" },
                    { 24, 21, "https://loremflickr.com/420/420/tool" },
                    { 25, 14, "https://loremflickr.com/420/420/tool" },
                    { 26, 23, "https://loremflickr.com/420/420/tool" },
                    { 27, 18, "https://loremflickr.com/420/420/tool" },
                    { 28, 7, "https://loremflickr.com/420/420/tool" },
                    { 29, 4, "https://loremflickr.com/420/420/tool" },
                    { 30, 17, "https://loremflickr.com/420/420/tool" },
                    { 31, 22, "https://loremflickr.com/420/420/tool" },
                    { 32, 17, "https://loremflickr.com/420/420/tool" },
                    { 33, 21, "https://loremflickr.com/420/420/tool" },
                    { 34, 18, "https://loremflickr.com/420/420/tool" },
                    { 35, 13, "https://loremflickr.com/420/420/tool" },
                    { 36, 24, "https://loremflickr.com/420/420/tool" },
                    { 37, 19, "https://loremflickr.com/420/420/tool" },
                    { 38, 9, "https://loremflickr.com/420/420/tool" },
                    { 39, 20, "https://loremflickr.com/420/420/tool" },
                    { 40, 18, "https://loremflickr.com/420/420/tool" },
                    { 41, 23, "https://loremflickr.com/420/420/tool" },
                    { 42, 12, "https://loremflickr.com/420/420/tool" },
                    { 43, 21, "https://loremflickr.com/420/420/tool" },
                    { 44, 21, "https://loremflickr.com/420/420/tool" },
                    { 45, 13, "https://loremflickr.com/420/420/tool" },
                    { 46, 22, "https://loremflickr.com/420/420/tool" },
                    { 47, 17, "https://loremflickr.com/420/420/tool" },
                    { 48, 22, "https://loremflickr.com/420/420/tool" },
                    { 49, 14, "https://loremflickr.com/420/420/tool" },
                    { 50, 19, "https://loremflickr.com/420/420/tool" },
                    { 51, 22, "https://loremflickr.com/420/420/tool" },
                    { 52, 28, "https://loremflickr.com/420/420/tool" },
                    { 53, 18, "https://loremflickr.com/420/420/tool" },
                    { 54, 26, "https://loremflickr.com/420/420/tool" },
                    { 55, 11, "https://loremflickr.com/420/420/tool" },
                    { 56, 17, "https://loremflickr.com/420/420/tool" },
                    { 57, 21, "https://loremflickr.com/420/420/tool" },
                    { 58, 27, "https://loremflickr.com/420/420/tool" },
                    { 59, 1, "https://loremflickr.com/420/420/tool" },
                    { 60, 19, "https://loremflickr.com/420/420/tool" },
                    { 61, 3, "https://loremflickr.com/420/420/tool" },
                    { 62, 19, "https://loremflickr.com/420/420/tool" },
                    { 63, 27, "https://loremflickr.com/420/420/tool" },
                    { 64, 17, "https://loremflickr.com/420/420/tool" },
                    { 65, 13, "https://loremflickr.com/420/420/tool" },
                    { 66, 6, "https://loremflickr.com/420/420/tool" },
                    { 67, 2, "https://loremflickr.com/420/420/tool" },
                    { 68, 21, "https://loremflickr.com/420/420/tool" },
                    { 69, 10, "https://loremflickr.com/420/420/tool" },
                    { 70, 17, "https://loremflickr.com/420/420/tool" },
                    { 71, 7, "https://loremflickr.com/420/420/tool" },
                    { 72, 14, "https://loremflickr.com/420/420/tool" },
                    { 73, 11, "https://loremflickr.com/420/420/tool" },
                    { 74, 10, "https://loremflickr.com/420/420/tool" },
                    { 75, 26, "https://loremflickr.com/420/420/tool" },
                    { 76, 13, "https://loremflickr.com/420/420/tool" },
                    { 77, 22, "https://loremflickr.com/420/420/tool" },
                    { 78, 15, "https://loremflickr.com/420/420/tool" },
                    { 79, 19, "https://loremflickr.com/420/420/tool" },
                    { 80, 12, "https://loremflickr.com/420/420/tool" },
                    { 81, 16, "https://loremflickr.com/420/420/tool" },
                    { 82, 22, "https://loremflickr.com/420/420/tool" },
                    { 83, 1, "https://loremflickr.com/420/420/tool" },
                    { 84, 28, "https://loremflickr.com/420/420/tool" },
                    { 85, 3, "https://loremflickr.com/420/420/tool" },
                    { 86, 14, "https://loremflickr.com/420/420/tool" },
                    { 87, 17, "https://loremflickr.com/420/420/tool" },
                    { 88, 29, "https://loremflickr.com/420/420/tool" },
                    { 89, 10, "https://loremflickr.com/420/420/tool" },
                    { 90, 25, "https://loremflickr.com/420/420/tool" }
                });

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
                name: "ItemPictures");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
