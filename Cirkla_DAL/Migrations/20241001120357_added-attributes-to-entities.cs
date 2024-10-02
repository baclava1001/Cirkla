using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class addedattributestoentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Items",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "ItemId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 2,
                column: "ItemId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 3,
                column: "ItemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 5,
                column: "ItemId",
                value: 26);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 6,
                column: "ItemId",
                value: 26);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 7,
                column: "ItemId",
                value: 19);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 8,
                column: "ItemId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 10,
                column: "ItemId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 11,
                column: "ItemId",
                value: 21);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 12,
                column: "ItemId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 13,
                column: "ItemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 14,
                column: "ItemId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 15,
                column: "ItemId",
                value: 26);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 16,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 18,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 19,
                column: "ItemId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 20,
                column: "ItemId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 21,
                column: "ItemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 22,
                column: "ItemId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 23,
                column: "ItemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 24,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 25,
                column: "ItemId",
                value: 27);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 26,
                column: "ItemId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 27,
                column: "ItemId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 28,
                column: "ItemId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 29,
                column: "ItemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 30,
                column: "ItemId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 31,
                column: "ItemId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 32,
                column: "ItemId",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 33,
                column: "ItemId",
                value: 25);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 34,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 35,
                column: "ItemId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 36,
                column: "ItemId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 37,
                column: "ItemId",
                value: 21);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 38,
                column: "ItemId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 39,
                column: "ItemId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 40,
                column: "ItemId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 41,
                column: "ItemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 42,
                column: "ItemId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 44,
                column: "ItemId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 45,
                column: "ItemId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 46,
                column: "ItemId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 47,
                column: "ItemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 48,
                column: "ItemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 49,
                column: "ItemId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 51,
                column: "ItemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 52,
                column: "ItemId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 53,
                column: "ItemId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 54,
                column: "ItemId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 55,
                column: "ItemId",
                value: 27);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 56,
                column: "ItemId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 57,
                column: "ItemId",
                value: 28);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 58,
                column: "ItemId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 59,
                column: "ItemId",
                value: 25);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 60,
                column: "ItemId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 61,
                column: "ItemId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 62,
                column: "ItemId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 63,
                column: "ItemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 64,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 65,
                column: "ItemId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 66,
                column: "ItemId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 67,
                column: "ItemId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 68,
                column: "ItemId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 69,
                column: "ItemId",
                value: 21);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 70,
                column: "ItemId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 71,
                column: "ItemId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 72,
                column: "ItemId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 73,
                column: "ItemId",
                value: 27);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 74,
                column: "ItemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 75,
                column: "ItemId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 76,
                column: "ItemId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 77,
                column: "ItemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 78,
                column: "ItemId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 79,
                column: "ItemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 80,
                column: "ItemId",
                value: 27);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 81,
                column: "ItemId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 82,
                column: "ItemId",
                value: 25);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 83,
                column: "ItemId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 84,
                column: "ItemId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 85,
                column: "ItemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 86,
                column: "ItemId",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 87,
                column: "ItemId",
                value: 25);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 88,
                column: "ItemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 89,
                column: "ItemId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 90,
                column: "ItemId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "OwnerId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "OwnerId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "OwnerId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12,
                column: "OwnerId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                column: "OwnerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14,
                column: "OwnerId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15,
                column: "OwnerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16,
                column: "OwnerId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17,
                column: "OwnerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18,
                column: "OwnerId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 19,
                column: "OwnerId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 21,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 23,
                column: "OwnerId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 24,
                column: "OwnerId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 26,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 27,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 28,
                column: "OwnerId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 29,
                column: "OwnerId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 30,
                column: "OwnerId",
                value: 9);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Items",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 1,
                column: "ItemId",
                value: 25);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 2,
                column: "ItemId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 3,
                column: "ItemId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 5,
                column: "ItemId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 6,
                column: "ItemId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 7,
                column: "ItemId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 8,
                column: "ItemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 10,
                column: "ItemId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 11,
                column: "ItemId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 12,
                column: "ItemId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 13,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 14,
                column: "ItemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 15,
                column: "ItemId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 16,
                column: "ItemId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 18,
                column: "ItemId",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 19,
                column: "ItemId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 20,
                column: "ItemId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 21,
                column: "ItemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 22,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 23,
                column: "ItemId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 24,
                column: "ItemId",
                value: 21);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 25,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 26,
                column: "ItemId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 27,
                column: "ItemId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 28,
                column: "ItemId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 29,
                column: "ItemId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 30,
                column: "ItemId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 31,
                column: "ItemId",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 32,
                column: "ItemId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 33,
                column: "ItemId",
                value: 21);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 34,
                column: "ItemId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 35,
                column: "ItemId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 36,
                column: "ItemId",
                value: 24);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 37,
                column: "ItemId",
                value: 19);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 38,
                column: "ItemId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 39,
                column: "ItemId",
                value: 20);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 40,
                column: "ItemId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 41,
                column: "ItemId",
                value: 23);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 42,
                column: "ItemId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 44,
                column: "ItemId",
                value: 21);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 45,
                column: "ItemId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 46,
                column: "ItemId",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 47,
                column: "ItemId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 48,
                column: "ItemId",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 49,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 51,
                column: "ItemId",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 52,
                column: "ItemId",
                value: 28);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 53,
                column: "ItemId",
                value: 18);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 54,
                column: "ItemId",
                value: 26);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 55,
                column: "ItemId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 56,
                column: "ItemId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 57,
                column: "ItemId",
                value: 21);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 58,
                column: "ItemId",
                value: 27);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 59,
                column: "ItemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 60,
                column: "ItemId",
                value: 19);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 61,
                column: "ItemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 62,
                column: "ItemId",
                value: 19);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 63,
                column: "ItemId",
                value: 27);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 64,
                column: "ItemId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 65,
                column: "ItemId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 66,
                column: "ItemId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 67,
                column: "ItemId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 68,
                column: "ItemId",
                value: 21);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 69,
                column: "ItemId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 70,
                column: "ItemId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 71,
                column: "ItemId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 72,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 73,
                column: "ItemId",
                value: 11);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 74,
                column: "ItemId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 75,
                column: "ItemId",
                value: 26);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 76,
                column: "ItemId",
                value: 13);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 77,
                column: "ItemId",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 78,
                column: "ItemId",
                value: 15);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 79,
                column: "ItemId",
                value: 19);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 80,
                column: "ItemId",
                value: 12);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 81,
                column: "ItemId",
                value: 16);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 82,
                column: "ItemId",
                value: 22);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 83,
                column: "ItemId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 84,
                column: "ItemId",
                value: 28);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 85,
                column: "ItemId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 86,
                column: "ItemId",
                value: 14);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 87,
                column: "ItemId",
                value: 17);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 88,
                column: "ItemId",
                value: 29);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 89,
                column: "ItemId",
                value: 10);

            migrationBuilder.UpdateData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 90,
                column: "ItemId",
                value: 25);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1,
                column: "OwnerId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2,
                column: "OwnerId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3,
                column: "OwnerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4,
                column: "OwnerId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6,
                column: "OwnerId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7,
                column: "OwnerId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9,
                column: "OwnerId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10,
                column: "OwnerId",
                value: 7);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12,
                column: "OwnerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13,
                column: "OwnerId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14,
                column: "OwnerId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16,
                column: "OwnerId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17,
                column: "OwnerId",
                value: 9);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18,
                column: "OwnerId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 19,
                column: "OwnerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20,
                column: "OwnerId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 21,
                column: "OwnerId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 23,
                column: "OwnerId",
                value: 8);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 24,
                column: "OwnerId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 26,
                column: "OwnerId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 27,
                column: "OwnerId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 28,
                column: "OwnerId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 29,
                column: "OwnerId",
                value: 6);

            migrationBuilder.UpdateData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 30,
                column: "OwnerId",
                value: 2);
        }
    }
}
