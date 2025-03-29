using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelMangementSystem.Migrations
{
    /// <inheritdoc />
    public partial class addHotelStatues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HotelStatus",
                table: "PendingHotels",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "HotelStatus",
                table: "Hotels",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HotelStatus",
                table: "PendingHotels");

            migrationBuilder.DropColumn(
                name: "HotelStatus",
                table: "Hotels");
        }
    }
}
