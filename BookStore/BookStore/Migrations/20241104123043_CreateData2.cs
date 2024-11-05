using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class CreateData2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_items_products_ProductId",
                table: "order_items");

            migrationBuilder.DropIndex(
                name: "IX_order_items_ProductId",
                table: "order_items");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "order_items");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "order_items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_items_ProductId",
                table: "order_items",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_items_products_ProductId",
                table: "order_items",
                column: "ProductId",
                principalTable: "products",
                principalColumn: "product_id");
        }
    }
}
