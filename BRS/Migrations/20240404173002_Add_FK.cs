using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRS.Migrations
{
    /// <inheritdoc />
    public partial class Add_FK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_RentHistory_RentId",
                table: "RentHistory",
                column: "RentId");

            migrationBuilder.CreateIndex(
                name: "IX_Books_StatusId",
                table: "Books",
                column: "StatusId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookRental_BookId",
                table: "BookRental",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookRental_UserId",
                table: "BookRental",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRental_Books_BookId",
                table: "BookRental",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookRental_Users_UserId",
                table: "BookRental",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_BookStatus_StatusId",
                table: "Books",
                column: "StatusId",
                principalTable: "BookStatus",
                principalColumn: "StatusId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RentHistory_BookRental_RentId",
                table: "RentHistory",
                column: "RentId",
                principalTable: "BookRental",
                principalColumn: "RentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookRental_Books_BookId",
                table: "BookRental");

            migrationBuilder.DropForeignKey(
                name: "FK_BookRental_Users_UserId",
                table: "BookRental");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_BookStatus_StatusId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_RentHistory_BookRental_RentId",
                table: "RentHistory");

            migrationBuilder.DropIndex(
                name: "IX_RentHistory_RentId",
                table: "RentHistory");

            migrationBuilder.DropIndex(
                name: "IX_Books_StatusId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_BookRental_BookId",
                table: "BookRental");

            migrationBuilder.DropIndex(
                name: "IX_BookRental_UserId",
                table: "BookRental");
        }
    }
}
