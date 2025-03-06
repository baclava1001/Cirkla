using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class CirclesAndCircleRequestsCORRECT4 : Migration
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

            migrationBuilder.AddForeignKey(
                name: "FK_CircleAdministrator_AspNetUsers_UserId",
                table: "CircleAdministrator",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CircleAdministrator_Circles_CircleId",
                table: "CircleAdministrator",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CircleMember_AspNetUsers_UserId",
                table: "CircleMember",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CircleMember_Circles_CircleId",
                table: "CircleMember",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
