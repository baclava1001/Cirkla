using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class AddedMoreTimestampsToContract : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptedByOwner",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeniedByOwner",
                table: "Contracts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptedByOwner",
                table: "Contracts");

            migrationBuilder.DropColumn(
                name: "DeniedByOwner",
                table: "Contracts");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1149f207-1f12-4aa6-aea0-480bbcc55af8", "AQAAAAIAAYagAAAAENpMP8xXTItwLpkj4383blGSkLoGNfVi9Ja3pFd01exSLIBcbphYATMyA1yKdFYkLQ==", "c057d159-cbc6-4a34-a161-17df6b88a50b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4457290b-9de8-4c26-a010-50ae3533c289", "AQAAAAIAAYagAAAAELNaoyH6b5xipCTWCaobWVUZoZuoPVwDs63A3LquQ4jC3BteQMjBGxrZBublSZ52kg==", "c2019ccf-c375-4663-a81e-3ff8a01e3b93" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2162ceb-793d-4e32-8029-ca56472dd93a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "97b77eaf-cbb8-4930-9445-3d69f3d53e2f", "AQAAAAIAAYagAAAAEHLuloBhHGjbIlpihwIz7F2UIXFcYU17HcpnuZ1ElZ7fD840MeWeF+VgF74dmjw3jw==", "d5ecd196-ed06-4806-8536-2f7de14528ca" });
        }
    }
}
