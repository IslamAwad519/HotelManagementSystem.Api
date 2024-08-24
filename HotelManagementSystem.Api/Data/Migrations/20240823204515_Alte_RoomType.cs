using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelManagementSystem.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alte_RoomType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceDouble",
                table: "RoomTypes");

            migrationBuilder.DropColumn(
                name: "PriceSingle",
                table: "RoomTypes");

            migrationBuilder.RenameColumn(
                name: "PriceTriple",
                table: "RoomTypes",
                newName: "Price");

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "RoomTypes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "RoomTypes");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "RoomTypes",
                newName: "PriceTriple");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceDouble",
                table: "RoomTypes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PriceSingle",
                table: "RoomTypes",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);
        }
    }
}
