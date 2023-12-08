using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JSandwiches.API.Migrations
{
    /// <inheritdoc />
    public partial class orderfixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_MenuItemAddOn_MenuItemAddOnID",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_MenuItemAddOnID",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "MenuItemAddOnID",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "OrderID",
                table: "MenuItemAddOn",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_MenuItemAddOn_OrderID",
                table: "MenuItemAddOn",
                column: "OrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuItemAddOn_Order_OrderID",
                table: "MenuItemAddOn",
                column: "OrderID",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MenuItemAddOn_Order_OrderID",
                table: "MenuItemAddOn");

            migrationBuilder.DropIndex(
                name: "IX_MenuItemAddOn_OrderID",
                table: "MenuItemAddOn");

            migrationBuilder.DropColumn(
                name: "OrderID",
                table: "MenuItemAddOn");

            migrationBuilder.AddColumn<int>(
                name: "MenuItemAddOnID",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Order_MenuItemAddOnID",
                table: "Order",
                column: "MenuItemAddOnID");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_MenuItemAddOn_MenuItemAddOnID",
                table: "Order",
                column: "MenuItemAddOnID",
                principalTable: "MenuItemAddOn",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
