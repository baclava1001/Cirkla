using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class CirclesAndCircleRequestsCORRECT2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircleRequests_AspNetUsers_FromUserId",
                table: "CircleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Circles_AspNetUsers_CreatedById",
                table: "Circles");

            migrationBuilder.DropTable(
                name: "CircleAdministrators");

            migrationBuilder.DropTable(
                name: "CircleMembers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Circles",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedByUserId",
                table: "CircleRequests",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CircleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CircleId1",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Circles_UserId",
                table: "Circles",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CircleRequests_UpdatedByUserId",
                table: "CircleRequests",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CircleId",
                table: "AspNetUsers",
                column: "CircleId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CircleId1",
                table: "AspNetUsers",
                column: "CircleId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Circles_CircleId",
                table: "AspNetUsers",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Circles_CircleId1",
                table: "AspNetUsers",
                column: "CircleId1",
                principalTable: "Circles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleRequests_AspNetUsers_FromUserId",
                table: "CircleRequests",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleRequests_AspNetUsers_UpdatedByUserId",
                table: "CircleRequests",
                column: "UpdatedByUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Circles_AspNetUsers_CreatedById",
                table: "Circles",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Circles_AspNetUsers_UserId",
                table: "Circles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Circles_CircleId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Circles_CircleId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleRequests_AspNetUsers_FromUserId",
                table: "CircleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleRequests_AspNetUsers_UpdatedByUserId",
                table: "CircleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Circles_AspNetUsers_CreatedById",
                table: "Circles");

            migrationBuilder.DropForeignKey(
                name: "FK_Circles_AspNetUsers_UserId",
                table: "Circles");

            migrationBuilder.DropIndex(
                name: "IX_Circles_UserId",
                table: "Circles");

            migrationBuilder.DropIndex(
                name: "IX_CircleRequests_UpdatedByUserId",
                table: "CircleRequests");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CircleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_CircleId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Circles");

            migrationBuilder.DropColumn(
                name: "CircleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CircleId1",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "UpdatedByUserId",
                table: "CircleRequests",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CircleAdministrators",
                columns: table => new
                {
                    AdministratorsId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CircleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircleAdministrators", x => new { x.AdministratorsId, x.CircleId });
                    table.ForeignKey(
                        name: "FK_CircleAdministrators_AspNetUsers_AdministratorsId",
                        column: x => x.AdministratorsId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CircleAdministrators_Circles_CircleId",
                        column: x => x.CircleId,
                        principalTable: "Circles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CircleMembers",
                columns: table => new
                {
                    Circle1Id = table.Column<int>(type: "int", nullable: false),
                    MembersId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircleMembers", x => new { x.Circle1Id, x.MembersId });
                    table.ForeignKey(
                        name: "FK_CircleMembers_AspNetUsers_MembersId",
                        column: x => x.MembersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CircleMembers_Circles_Circle1Id",
                        column: x => x.Circle1Id,
                        principalTable: "Circles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CircleAdministrators_CircleId",
                table: "CircleAdministrators",
                column: "CircleId");

            migrationBuilder.CreateIndex(
                name: "IX_CircleMembers_MembersId",
                table: "CircleMembers",
                column: "MembersId");

            migrationBuilder.AddForeignKey(
                name: "FK_CircleRequests_AspNetUsers_FromUserId",
                table: "CircleRequests",
                column: "FromUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Circles_AspNetUsers_CreatedById",
                table: "Circles",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
