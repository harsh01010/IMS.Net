using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Services.OrderAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedOrderValueInOrdersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "OrderValue",
                table: "Orders",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderValue",
                table: "Orders");
        }
    }
}
