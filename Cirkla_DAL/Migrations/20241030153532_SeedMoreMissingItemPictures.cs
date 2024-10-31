using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedMoreMissingItemPictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "572a48eb-270b-4d4f-9687-7f0103cd9bfb", "AQAAAAIAAYagAAAAEA7sRIrjNqy8xvCYrLFkB373r3lImJbEytQ8AuVCPwmgypFxVB81aYTyjCK5zvk+1g==", "4227d87b-a3f9-4b7e-9815-4cc4523af276" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "69f9b96d-f027-44da-8335-fe6219819fbe", "AQAAAAIAAYagAAAAELR76dgDyw73Dj0A3p3fxapXfxbMTHRDSAHIFID8ZU0Ef1tzdBE3AZ++0bh0KqKzfw==", "fec86636-6b26-4e5a-bb19-7d493c5a2ed8" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2162ceb-793d-4e32-8029-ca56472dd93a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cac9f704-68c5-44b1-9960-96a2853540d0", "AQAAAAIAAYagAAAAEIyHlp0EanXKgaBMpddhqHKwq7NawQmJy/j1PGdfer5/fMlbWfZTm9HU1tSh25IQvQ==", "5c906cca-3a1a-48f1-b8b3-ba3aac83ac21" });

            migrationBuilder.InsertData(
                table: "ItemPictures",
                columns: new[] { "Id", "ItemId", "Url" },
                values: new object[,]
                {
                    { 34, 9, "https://cdn.mos.cms.futurecdn.net/cfxJWdTTkVAUXFxsdvTy3n-320-80.png" },
                    { 35, 9, "https://www.cnet.com/a/img/resize/bebef835df90640f9aa2e4a2f2a2699cf53a301f/hub/2020/10/26/b60bfe6f-3193-4381-b0d4-ac628cdcc565/img-1419.jpg?auto=webp&width=1200" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 34);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 35);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "54b5627b-1f8e-4634-8bb0-206fecc840f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ae5833dd-0c22-4a22-ac90-cc07398701a8", "AQAAAAIAAYagAAAAEHvMy9z3iBjIhdGk/ix2Wdmt5YenDUwjVRf1/ko2c5N4U+Hc1IOThYzBNABk+OTipQ==", "3b080feb-b70b-49b1-a141-64bd00acfc9c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6ce14244-d9f8-417e-b05f-df87f2c044e4",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "556523b4-ec8e-402e-a548-66830d4ba1c1", "AQAAAAIAAYagAAAAEE2N4LiIr4SEw0z8vn/sfvPAs+owOR5aJbTfcm1Fp+45hAkS5CefL6VzyYidun9Bow==", "881359e1-8a7d-4f75-b742-ebf5f51ac98d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b2162ceb-793d-4e32-8029-ca56472dd93a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4b83e84f-dd9e-4600-9c9c-3ef0f530100e", "AQAAAAIAAYagAAAAEFyy08bUiPhca9hPqciZJMVgO2QsgBMuUIbmtsbJ+WKuVjHKPXD3HXlEh1qkKVZWWQ==", "da0a1935-ee47-418f-8d5b-2efd718bc6fa" });
        }
    }
}
