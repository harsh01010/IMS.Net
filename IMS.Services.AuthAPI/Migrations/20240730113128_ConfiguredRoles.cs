using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IMS.Services.AuthAPI.Migrations
{
    /// <inheritdoc />
    public partial class ConfiguredRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "12fcb3da-d86a-4dd0-a5f3-1d448cda0a1f", "12fcb3da-d86a-4dd0-a5f3-1d448cda0a1f", "Customer", "CUSTOMER" },
                    { "50f2e492-9b92-403e-9dcf-d8ac7b6d7100", "50f2e492-9b92-403e-9dcf-d8ac7b6d7100", "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "12fcb3da-d86a-4dd0-a5f3-1d448cda0a1f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "50f2e492-9b92-403e-9dcf-d8ac7b6d7100");
        }
    }
}
