using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JSandwiches.API.Migrations
{
    /// <inheritdoc />
    public partial class updatePayment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_DealSpecifics_DealSpecificsID",
                table: "Receipt");

            migrationBuilder.AlterColumn<int>(
                name: "DealSpecificsID",
                table: "Receipt",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReceiptNumber",
                table: "Payment",
                type: "varchar(MAX)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_DealSpecifics_DealSpecificsID",
                table: "Receipt",
                column: "DealSpecificsID",
                principalTable: "DealSpecifics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipt_DealSpecifics_DealSpecificsID",
                table: "Receipt");

            migrationBuilder.DropColumn(
                name: "ReceiptNumber",
                table: "Payment");

            migrationBuilder.AlterColumn<int>(
                name: "DealSpecificsID",
                table: "Receipt",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipt_DealSpecifics_DealSpecificsID",
                table: "Receipt",
                column: "DealSpecificsID",
                principalTable: "DealSpecifics",
                principalColumn: "Id");
        }
    }
}
