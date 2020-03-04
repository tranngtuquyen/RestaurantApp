using Microsoft.EntityFrameworkCore.Migrations;

namespace RestaurantApp.Migrations
{
    public partial class _2020030201 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_Orders_OrderID1",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Receipts_OrderID1",
                table: "Receipts");

            migrationBuilder.DropColumn(
                name: "OrderID1",
                table: "Receipts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderID1",
                table: "Receipts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_OrderID1",
                table: "Receipts",
                column: "OrderID1",
                unique: true,
                filter: "[OrderID1] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_Orders_OrderID1",
                table: "Receipts",
                column: "OrderID1",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
