using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BRS.Migrations
{
    /// <inheritdoc />
    public partial class seedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Inventory",
                columns: new[] { "InventoryId", "AvailableBooks", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "ReservedBooks", "TotalBooks", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("3758ec16-9d7f-4b4c-8ad6-a3ce9ba1e587"), 0, null, null, null, null, false, 0, 0, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Inventory",
                keyColumn: "InventoryId",
                keyValue: new Guid("3758ec16-9d7f-4b4c-8ad6-a3ce9ba1e587"));
        }
    }
}
