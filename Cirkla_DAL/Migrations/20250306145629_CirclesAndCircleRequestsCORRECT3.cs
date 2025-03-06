using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class CirclesAndCircleRequestsCORRECT3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "FK_CircleRequests_Circles_CircleId",
                table: "CircleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Circles_AspNetUsers_CreatedById",
                table: "Circles");

            migrationBuilder.DropForeignKey(
                name: "FK_Circles_AspNetUsers_UpdatedById",
                table: "Circles");

            migrationBuilder.DropForeignKey(
                name: "FK_Circles_AspNetUsers_UserId",
                table: "Circles");

            migrationBuilder.DropIndex(
                name: "IX_Circles_UserId",
                table: "Circles");

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

            migrationBuilder.CreateTable(
                name: "CircleAdministrator",
                columns: table => new
                {
                    CircleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircleAdministrator", x => new { x.CircleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CircleAdministrator_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CircleAdministrator_Circles_CircleId",
                        column: x => x.CircleId,
                        principalTable: "Circles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CircleMember",
                columns: table => new
                {
                    CircleId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CircleMember", x => new { x.CircleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CircleMember_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CircleMember_Circles_CircleId",
                        column: x => x.CircleId,
                        principalTable: "Circles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CircleAdministrator_UserId",
                table: "CircleAdministrator",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CircleMember_UserId",
                table: "CircleMember",
                column: "UserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Circles_AspNetUsers_CreatedById",
                table: "Circles",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Circles_AspNetUsers_UpdatedById",
                table: "Circles",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CircleRequests_AspNetUsers_FromUserId",
                table: "CircleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_CircleRequests_Circles_CircleId",
                table: "CircleRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Circles_AspNetUsers_CreatedById",
                table: "Circles");

            migrationBuilder.DropForeignKey(
                name: "FK_Circles_AspNetUsers_UpdatedById",
                table: "Circles");

            migrationBuilder.DropTable(
                name: "CircleAdministrator");

            migrationBuilder.DropTable(
                name: "CircleMember");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Circles",
                type: "nvarchar(450)",
                nullable: true);

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
                name: "FK_CircleRequests_Circles_CircleId",
                table: "CircleRequests",
                column: "CircleId",
                principalTable: "Circles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Circles_AspNetUsers_CreatedById",
                table: "Circles",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Circles_AspNetUsers_UpdatedById",
                table: "Circles",
                column: "UpdatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Circles_AspNetUsers_UserId",
                table: "Circles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
