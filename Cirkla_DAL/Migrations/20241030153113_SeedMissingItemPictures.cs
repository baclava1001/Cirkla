using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class SeedMissingItemPictures : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "ItemPictures",
                columns: new[] { "Id", "ItemId", "Url" },
                values: new object[,]
                {
                    { 27, 7, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS-IvVN7S0UFhWV6z-n4RBaZ1YxoPZikj3ODA&s" },
                    { 28, 7, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQD4qVS5aq3WC6EzFq1Dd--ggCn-FHrnMeXIQ&s" },
                    { 29, 7, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTCQpV8waVQTS2y8XXgXJX4H0L6biHgv9BfUA&s" },
                    { 30, 8, "https://i1.adis.ws/i/canon/eos-r5_martin_bissig_lifestyle_05_c629aad3c2154f34b3d7d7ba5a509196?$70-30-header-4by3-dt-jpg$" },
                    { 31, 8, "https://i1.adis.ws/i/canon/eos-r5-c_lifestyle_47-pro_7e76ebaee6314ff9a06d7f00f59f0d1a9" },
                    { 32, 8, "https://cdn.mos.cms.futurecdn.net/cfxJWdTTkVAUXFxsdvTy3n-320-80.png" },
                    { 33, 10, "https://cdn.mos.cms.futurecdn.net/xaMxSfTmuD8nbaVGprXwS4.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 29);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 30);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 31);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 32);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 33);

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
    }
}
