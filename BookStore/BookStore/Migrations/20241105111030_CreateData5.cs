using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class CreateData5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders",
                table: "order_items");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "order_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders",
                table: "order_items");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders",
                table: "order_items",
                column: "order_id",
                principalTable: "orders",
                principalColumn: "order_id");
        }
    }
}
