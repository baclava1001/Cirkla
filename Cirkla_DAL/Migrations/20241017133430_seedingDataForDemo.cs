using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Cirkla_DAL.Migrations
{
    /// <inheritdoc />
    public partial class seedingDataForDemo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Category", "Description", "Model", "Name", "OwnerId", "Specifications" },
                values: new object[,]
                {
                    { 1, "Electronics", "Tracks fitness and health metrics.", "Polar Grit X2 Pro Titanium Leather Bronze", "Smartwatch", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "GPS + Cellular, 45mm" },
                    { 2, "Electronics", "Premium laptop for professionals.", "Dell XPS 15", "High-end Laptop", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "Intel i7, 16GB RAM, 512GB SSD" },
                    { 3, "Personal stuff", "A timeless and spacious designer bag.", "Louis Vuitton Neverfull", "Designer Bag", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "Monogram Canvas, Leather trim" },
                    { 4, "Electronics", "High-end headphones with superior noise cancellation.", "Bose 700", "Noise Cancelling Headphones", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "Bluetooth, Noise Cancelling" },
                    { 5, "Electronics", "Top-tier smartphone with advanced features.", "iPhone 13 Pro Max", "Smartphone", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "256GB, 6.7-inch display" },
                    { 6, "Personal stuff", "Iconic luxury diving watch.", "Rolex Submariner", "Luxury Watch", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "Automatic, Stainless Steel" },
                    { 7, "Transportation", "High-performance electric scooter.", "Segway Ninebot Max", "Electric Scooter", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "350W Motor, 40 miles range" },
                    { 8, "Electronics", "Professional-grade mirrorless camera.", "Canon EOS R5", "Camera", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "45MP, 8K Video" },
                    { 9, "Electronics", "Next-gen gaming console.", "PlayStation 5", "Gaming Console", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "825GB SSD, 4K Gaming" },
                    { 10, "Electronics", "High-end soundbar for immersive audio.", "Sonos Arc", "Sound System", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "Dolby Atmos, Wi-Fi" },
                    { 11, "Electronics", "State-of-the-art television with stunning picture quality.", "Samsung QLED", "4K TV", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "65-inch, 4K UHD" },
                    { 12, "Personal stuff", "Iconic fountain pen with exceptional craftsmanship.", "Montblanc Meisterstück", "Luxury Pen", "b2162ceb-793d-4e32-8029-ca56472dd93a", "Resin Barrel, Gold Trim" },
                    { 13, "Transportation", "Luxury electric car with advanced features.", "Tesla Model S", "Electric Car", "b2162ceb-793d-4e32-8029-ca56472dd93a", "Long Range, Autopilot" },
                    { 14, "Electronics", "Smart display with built-in assistant.", "Google Nest Hub Max", "Smart Home Speaker", "b2162ceb-793d-4e32-8029-ca56472dd93a", "10-inch Display, Google Assistant" },
                    { 15, "Houshold & tools", "High-end air purifier for cleaner air.", "Dyson Pure Cool", "Air Purifier", "b2162ceb-793d-4e32-8029-ca56472dd93a", "HEPA Filter, Wi-Fi Enabled" },
                    { 16, "Electronics", "Premium portable speaker with excellent sound quality.", "Bang & Olufsen Beosound", "Bluetooth Speaker", "b2162ceb-793d-4e32-8029-ca56472dd93a", "360-degree sound, Portable" },
                    { 17, "Electronics", "High-performance drone for aerial photography.", "DJI Mavic Air 2", "Drone", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "4K Camera, 34 min flight time" },
                    { 18, "Electronics", "Immersive virtual reality experience.", "Oculus Quest 2", "VR Headset", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "128GB, All-in-One VR" },
                    { 19, "Houshold & tools", "Smart thermostat for energy-efficient home control.", "Nest Learning Thermostat", "Smart Thermostat", "54b5627b-1f8e-4634-8bb0-206fecc840f3", "Self-Learning, Wi-Fi" },
                    { 20, "Houshold & tools", "Professional-grade coffee maker for home baristas.", "Breville Barista Express", "High-End Coffee Maker", "6ce14244-d9f8-417e-b05f-df87f2c044e4", "Espresso Machine, Built-in Grinder" }
                });

            migrationBuilder.InsertData(
                table: "ItemPictures",
                columns: new[] { "Id", "ItemId", "Url" },
                values: new object[,]
                {
                    { 1, 1, "https://www.klockmagasinet.com/media/catalog/product/m/a/main_0.jpg?width=700&height=700&store=kk_se&image-type=image" },
                    { 2, 1, "https://www.klockmagasinet.com/media/catalog/product/p/r/product_10_0.jpg?width=700&height=700&store=kk_se&image-type=image" },
                    { 3, 2, "https://www.notebookcheck.se/uploads/tx_nbc2/DellXPS15-9510__1__02.jpg" },
                    { 4, 2, "https://www.pcworld.com/wp-content/uploads/2024/04/dell-xps-15-2023-2.jpg?resize=1024%2C683&quality=50&strip=all" },
                    { 5, 3, "https://www.careofcarl.se/bilder/artiklar/zoom/26143210_2.jpg?m=1702468978" },
                    { 6, 3, "https://i.ebayimg.com/images/g/HLwAAOSwFEVmIFwF/s-l400.png" },
                    { 7, 4, "https://img.tradera.net/large-fit/284/566851284_63fb4de8-6726-4f78-aed4-5e6fa4001cbe.jpg" },
                    { 8, 5, "https://wp.inews.co.uk/wp-content/uploads/2021/09/PRI_200908332-760x570.jpg" },
                    { 9, 6, "https://nymansur.commerce.services/product/raw/rolex-m126613lb-0002_2.png" },
                    { 10, 6, "https://nymansur.commerce.services/product/raw/rolex-m126613lb-0002_3.png" },
                    { 11, 11, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRaHm7uez_EIYmAIWKJ-CfENMtQPYmoPKHF5w&s" },
                    { 12, 12, "https://listerhorsfall.co.uk/wp-content/uploads/2024/06/MB131344-7.jpg" },
                    { 13, 13, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRoT9RUL5jsZ8G8HwhLIGMBtBBAeOcJphAcPg&s" },
                    { 14, 14, "https://owp.klarna.com/product/640x640/3054959225/Google-Nest-Hub-Max.jpg?ph=true" },
                    { 15, 15, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSnGtrrzm4FCTJW1eDrb9M1MJoH7JMV5yeiNQ&s" },
                    { 16, 15, "https://the-gadgeteer.com/wp-content/uploads/2019/06/Dyson_Pure_Cool_16.jpg" },
                    { 17, 16, "https://images.hifiklubben.com/image/e4cf3121-50ab-432d-8a35-b4a17ec3b7ee" },
                    { 18, 16, "https://images.hifiklubben.com/image/bb9d43a0-2886-47b5-9b41-cd11322a16ea" },
                    { 19, 17, "https://cdn.mos.cms.futurecdn.net/2mjes2QKryVCmU9dEReL6L.jpg" },
                    { 20, 17, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRMRuOgb22anrQFVd-1301SIV4fLRD_0sblbuGpudh1bj6-pd9c4I7u-t-q9K-U9dEcexc&usqp=CAU" },
                    { 21, 18, "https://cdn.mos.cms.futurecdn.net/zzjJ4bNcLthVTd6pTamotH-1920-80.jpg.webp" },
                    { 22, 18, "https://cdn.mos.cms.futurecdn.net/F4nXfc5jX5oVYUFNwudJa3-970-80.jpg.webp" },
                    { 23, 18, "https://cdn.mos.cms.futurecdn.net/p26Dp34kLtLuWy52VV6xz3-970-80.jpg.webp" },
                    { 24, 19, "https://www.intelligentabodes.co.uk/wp-content/uploads/2019/02/NEST-learning-thermostats-intelligent-abodes.jpg" },
                    { 25, 20, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQJjWQk4K16rCDWrXZCi9smr43_wb299Ke-FQ&s" },
                    { 26, 20, "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRPpAUvCna2bt1kS3tXBhgQ4GQDQMBv6gEfMlwS2Xyw1Xam1BGvzhGdt2BKpNCYQWGDwsU&usqp=CAU" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "ItemPictures",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Items",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
