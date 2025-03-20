using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class FixedTypos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircleJoinRequests_AspNetUsers_TargetMemberId",
                table: "CircleJoinRequests");

            migrationBuilder.RenameColumn(
                name: "TargetMemberId",
                table: "CircleJoinRequests",
                newName: "TargetUserId");

            migrationBuilder.RenameIndex(
                name: "IX_CircleJoinRequests_TargetMemberId",
                table: "CircleJoinRequests",
                newName: "IX_CircleJoinRequests_TargetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleJoinRequests_AspNetUsers_TargetUserId",
                table: "CircleJoinRequests",
                column: "TargetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircleJoinRequests_AspNetUsers_TargetUserId",
                table: "CircleJoinRequests");

            migrationBuilder.RenameColumn(
                name: "TargetUserId",
                table: "CircleJoinRequests",
                newName: "TargetMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_CircleJoinRequests_TargetUserId",
                table: "CircleJoinRequests",
                newName: "IX_CircleJoinRequests_TargetMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleJoinRequests_AspNetUsers_TargetMemberId",
                table: "CircleJoinRequests",
                column: "TargetMemberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
