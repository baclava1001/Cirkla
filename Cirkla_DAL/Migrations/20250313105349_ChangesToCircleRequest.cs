using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class ChangesToCircleRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircleRequests_AspNetUsers_FromUserId",
                table: "CircleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleRequests_Circles_CircleId",
                table: "CircleRequests");

            migrationBuilder.AddColumn<string>(
                name: "PendingMemberId",
                table: "CircleRequests",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_CircleRequests_PendingMemberId",
                table: "CircleRequests",
                column: "PendingMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleRequests_AspNetUsers_FromUserId",
                table: "CircleRequests",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleRequests_AspNetUsers_PendingMemberId",
                table: "CircleRequests",
                column: "PendingMemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleRequests_Circles_CircleId",
                table: "CircleRequests",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircleRequests_AspNetUsers_FromUserId",
                table: "CircleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleRequests_AspNetUsers_PendingMemberId",
                table: "CircleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleRequests_Circles_CircleId",
                table: "CircleRequests");

            migrationBuilder.DropIndex(
                name: "IX_CircleRequests_PendingMemberId",
                table: "CircleRequests");

            migrationBuilder.DropColumn(
                name: "PendingMemberId",
                table: "CircleRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleRequests_AspNetUsers_FromUserId",
                table: "CircleRequests",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CircleRequests_Circles_CircleId",
                table: "CircleRequests",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
