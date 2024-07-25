using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class HotelChangesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Hotels",
                newName: "Amenities");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Amenities",
                table: "Hotels",
                newName: "Price");
        }
    }
}
