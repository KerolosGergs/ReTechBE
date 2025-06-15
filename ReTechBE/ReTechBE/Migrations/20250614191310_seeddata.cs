using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReTechBE.Migrations
{
    /// <inheritdoc />
    public partial class seeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "RecyclingRequests",
                columns: new[] { "Id", "AssignedCompanyId", "CompletedDate", "CreatedAt", "CustomerId", "Description", "DeviceCondition", "DeviceModel", "DeviceType", "EstimatedValue", "ImageUrl", "ItemDescription", "Location", "Notes", "PickupAddress", "PickupDate", "RequestDate", "ScheduledDate", "Status", "UpdatedAt" },
                values: new object[,]
                {
                    { "REQ1", null, null, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "90a55d4a-547c-435e-a033-e369092bb87c", "Some scratches on the back", "Good", "iPhone 12", "Smartphone", 500m, "/images/recycling/iphone12.jpg", "Used iPhone 12 with minor scratches", "Cairo", null, "Downtown Branch", null, new DateTime(2024, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, null },
                    { "REQ2", "a21c50ad-4b69-4b0f-bd61-18d11425d910", null, new DateTime(2024, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "90a55d4a-547c-435e-a033-e369092bb87c", "Needs battery replacement", "Fair", "MacBook Pro 2019", "Laptop", 800m, "/images/recycling/macbook.jpg", "MacBook Pro with battery issues", "Giza", null, "6th October Branch", new DateTime(2024, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RecyclingRequests",
                keyColumn: "Id",
                keyValue: "REQ1");

            migrationBuilder.DeleteData(
                table: "RecyclingRequests",
                keyColumn: "Id",
                keyValue: "REQ2");
        }
    }
}
