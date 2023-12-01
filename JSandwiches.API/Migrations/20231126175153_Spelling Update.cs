using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JSandwiches.API.Migrations
{
    /// <inheritdoc />
    public partial class SpellingUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reciept");

            migrationBuilder.CreateTable(
                name: "Receipt",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReceiptNumber = table.Column<string>(type: "varchar(MAX)", nullable: false),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    DealSpecificsID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Receipt_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipt_DealSpecifics_DealSpecificsID",
                        column: x => x.DealSpecificsID,
                        principalTable: "DealSpecifics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Receipt_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_CustomerID",
                table: "Receipt",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_DealSpecificsID",
                table: "Receipt",
                column: "DealSpecificsID");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_OrderID",
                table: "Receipt",
                column: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Receipt");

            migrationBuilder.CreateTable(
                name: "Reciept",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: false),
                    DealSpecificsID = table.Column<int>(type: "int", nullable: true),
                    OrderID = table.Column<int>(type: "int", nullable: false),
                    RecieptNumber = table.Column<string>(type: "varchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reciept", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reciept_Customer_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customer",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reciept_DealSpecifics_DealSpecificsID",
                        column: x => x.DealSpecificsID,
                        principalTable: "DealSpecifics",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reciept_Order_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reciept_CustomerID",
                table: "Reciept",
                column: "CustomerID");

            migrationBuilder.CreateIndex(
                name: "IX_Reciept_DealSpecificsID",
                table: "Reciept",
                column: "DealSpecificsID");

            migrationBuilder.CreateIndex(
                name: "IX_Reciept_OrderID",
                table: "Reciept",
                column: "OrderID");
        }
    }
}
