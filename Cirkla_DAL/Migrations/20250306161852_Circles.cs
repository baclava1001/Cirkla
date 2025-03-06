using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Circles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPictures_Items_ItemId",
                table: "ItemPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPictures_Items_ItemId",
                table: "ItemPictures",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPictures_Items_ItemId",
                table: "ItemPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPictures_Items_ItemId",
                table: "ItemPictures",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
