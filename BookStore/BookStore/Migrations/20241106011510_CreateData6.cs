using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class CreateData6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Products",
                table: "inventory");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Products",
                table: "inventory",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Products",
                table: "inventory");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Products",
                table: "inventory",
                column: "product_id",
                principalTable: "products",
                principalColumn: "product_id");
        }
    }
}
