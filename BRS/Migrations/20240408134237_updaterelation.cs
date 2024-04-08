using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRS.Migrations
{
    /// <inheritdoc />
    public partial class updaterelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookRental_UserId",
                table: "BookRental");

            migrationBuilder.CreateIndex(
                name: "IX_BookRental_UserId",
                table: "BookRental",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BookRental_UserId",
                table: "BookRental");

            migrationBuilder.CreateIndex(
                name: "IX_BookRental_UserId",
                table: "BookRental",
                column: "UserId",
                unique: true);
        }
    }
}
