using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class Circles2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircleAdministrator_AspNetUsers_UserId",
                table: "CircleAdministrator");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleAdministrator_Circles_CircleId",
                table: "CircleAdministrator");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleMember_AspNetUsers_UserId",
                table: "CircleMember");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleMember_Circles_CircleId",
                table: "CircleMember");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_BorrowerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_OwnerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPictures_Items_ItemId",
                table: "ItemPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleAdministrator_AspNetUsers_UserId",
                table: "CircleAdministrator",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CircleAdministrator_Circles_CircleId",
                table: "CircleAdministrator",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CircleMember_AspNetUsers_UserId",
                table: "CircleMember",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CircleMember_Circles_CircleId",
                table: "CircleMember",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_BorrowerId",
                table: "Contracts",
                column: "BorrowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_OwnerId",
                table: "Contracts",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPictures_Items_ItemId",
                table: "ItemPictures",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircleAdministrator_AspNetUsers_UserId",
                table: "CircleAdministrator");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleAdministrator_Circles_CircleId",
                table: "CircleAdministrator");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleMember_AspNetUsers_UserId",
                table: "CircleMember");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleMember_Circles_CircleId",
                table: "CircleMember");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_BorrowerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_Contracts_AspNetUsers_OwnerId",
                table: "Contracts");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPictures_Items_ItemId",
                table: "ItemPictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_OwnerId",
                table: "Items");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleAdministrator_AspNetUsers_UserId",
                table: "CircleAdministrator",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleAdministrator_Circles_CircleId",
                table: "CircleAdministrator",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleMember_AspNetUsers_UserId",
                table: "CircleMember",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleMember_Circles_CircleId",
                table: "CircleMember",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_BorrowerId",
                table: "Contracts",
                column: "BorrowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Contracts_AspNetUsers_OwnerId",
                table: "Contracts",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

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
    }
}
