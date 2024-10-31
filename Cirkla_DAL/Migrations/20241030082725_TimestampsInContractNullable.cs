using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class TimestampsInContractNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeniedByOwner",
                table: "Contracts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AcceptedByOwner",
                table: "Contracts",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "93770194-65bd-435d-88af-bd7e59f58aba", "AQAAAAIAAYagAAAAEAu1UvAHTq5RX4ZtD0w/KpI1AD75IJVmpraMndQvLvtwWGwtdfWg/6BvyLgjYGMc1Q==", "4cab1cd4-0f73-4cbd-89e4-8d9f2d271cae" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c0dc0237-6bb8-440e-aaf0-76bf7d59f900", "AQAAAAIAAYagAAAAEKK41BspZx7xzF2s6M4BX2vN7Y9H+lr1FZMJCkENuY+//n96uqr3YPNQgqWwDilvIA==", "3f3fdb64-6f36-479b-b407-e68170a03fbe" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2162ceb-793d-4e32-8029-ca56472dd93a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d057346f-ba65-45cc-9bb8-b8f34afed726", "AQAAAAIAAYagAAAAEJ/AYcqiyiU9xWQ6TeavBndjGN4/hdvj6XKX4LPekcmHuP2/CNWuTWUr5X/uCCIirA==", "78ee19b6-fcb0-4326-bbfc-027f30d7950f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DeniedByOwner",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "AcceptedByOwner",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "139b1545-6060-4c19-9ff5-10e53662e7a0", "AQAAAAIAAYagAAAAEF0Mn+uowqs5yimQQuHMYSKp9HjlytJJXBBuPp52BX9De9ZhcGokk3FSVKowZJd18Q==", "6b48315f-0455-469d-a501-dbb67e5b7f41" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8eddd140-9fd4-4626-8197-bbd8399230a6", "AQAAAAIAAYagAAAAEAjIsYypZZ7WSA6K/PGL1InynyGg+AHqHLkgktEkOpaYTqiDM9icbteA87YexVxg+Q==", "493e9692-f3e7-4f26-a57a-a39a453f0bc9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2162ceb-793d-4e32-8029-ca56472dd93a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "afd728a0-519a-44c3-a259-306195a4429b", "AQAAAAIAAYagAAAAECC+Yhc8FpO31MIUK74/ClLuuYYrCfWRURnCgZgsfIJeySKCL6v9U8Uypoo9hCUlQw==", "36596aed-28d7-4eef-95b4-427cea70bc05" });
        }
    }
}
