using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace IMS.API.Migrations
{
    /// <inheritdoc />
    public partial class addedCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("468aa00d-12dd-423b-bae2-1d76ac141d2a"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("499b52f3-1092-4d9e-936b-84c9a2d0aa20"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("b1db29ae-e228-442e-98f8-18bfe977d2da"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("da0eb0c7-df49-446d-a0f5-74e00da148c0"));

            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[] { new Guid("e2693675-9acd-4556-b1e2-b0dae65658a6"), "Appetizer" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AvailableQuantity", "CategoryId", "Description", "ImageLocalPath", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0abee973-8514-4ba1-abc0-829a65e9ad26"), 10, new Guid("e2693675-9acd-4556-b1e2-b0dae65658a6"), " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", null, "https://placehold.co/603x403", "Samosa", 15.0 },
                    { new Guid("461bce40-afcf-4235-aa76-7a1a9bf24fb3"), 10, new Guid("e2693675-9acd-4556-b1e2-b0dae65658a6"), " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", null, "https://placehold.co/602x402", "Paneer Tikka", 13.99 },
                    { new Guid("94e85b9a-3a94-4b93-bf08-62b340101915"), 10, new Guid("e2693675-9acd-4556-b1e2-b0dae65658a6"), " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", null, "https://placehold.co/601x401", "Sweet Pie", 10.99 },
                    { new Guid("b09b6361-c345-4041-a61c-5e2e057bbbb2"), 10, new Guid("e2693675-9acd-4556-b1e2-b0dae65658a6"), " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", null, "https://placehold.co/600x400", "Pav Bhaji", 15.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("0abee973-8514-4ba1-abc0-829a65e9ad26"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("461bce40-afcf-4235-aa76-7a1a9bf24fb3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("94e85b9a-3a94-4b93-bf08-62b340101915"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ProductId",
                keyValue: new Guid("b09b6361-c345-4041-a61c-5e2e057bbbb2"));

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ProductId", "AvailableQuantity", "CategoryName", "Description", "ImageLocalPath", "ImageUrl", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("468aa00d-12dd-423b-bae2-1d76ac141d2a"), 10, "Appetizer", " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", null, "https://placehold.co/602x402", "Paneer Tikka", 13.99 },
                    { new Guid("499b52f3-1092-4d9e-936b-84c9a2d0aa20"), 10, "Dessert", " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", null, "https://placehold.co/601x401", "Sweet Pie", 10.99 },
                    { new Guid("b1db29ae-e228-442e-98f8-18bfe977d2da"), 10, "Entree", " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", null, "https://placehold.co/600x400", "Pav Bhaji", 15.0 },
                    { new Guid("da0eb0c7-df49-446d-a0f5-74e00da148c0"), 10, "Appetizer", " Quisque vel lacus ac magna, vehicula sagittis ut non lacus.<br/> Vestibulum arcu turpis, maximus malesuada neque. Phasellus commodo cursus pretium.", null, "https://placehold.co/603x403", "Samosa", 15.0 }
                });
        }
    }
}
