using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.Services.OrderAPI.Migrations.ShippingAddressDb
{
    /// <inheritdoc />
    public partial class addedShippingAddressesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ShippingAddresses",
                columns: table => new
                {
                    shippingAddressId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    userId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    houseNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    pinCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShippingAddresses", x => x.shippingAddressId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ShippingAddresses");
        }
    }
}
