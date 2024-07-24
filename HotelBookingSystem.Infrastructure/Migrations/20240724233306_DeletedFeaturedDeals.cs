using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelBookingSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DeletedFeaturedDeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FeaturedDeals");

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountedPrice",
                table: "Rooms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "FeaturedDeal",
                table: "Rooms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DiscountedPrice",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "FeaturedDeal",
                table: "Rooms");

            migrationBuilder.CreateTable(
                name: "FeaturedDeals",
                columns: table => new
                {
                    FeaturedDealId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    HotelId = table.Column<int>(type: "INTEGER", nullable: false),
                    DiscountedPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    OriginalPrice = table.Column<decimal>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeaturedDeals", x => x.FeaturedDealId);
                    table.ForeignKey(
                        name: "FK_FeaturedDeals_Hotels_HotelId",
                        column: x => x.HotelId,
                        principalTable: "Hotels",
                        principalColumn: "HotelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FeaturedDeals_HotelId",
                table: "FeaturedDeals",
                column: "HotelId");
        }
    }
}
