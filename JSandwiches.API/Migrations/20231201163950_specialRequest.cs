using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JSandwiches.API.Migrations
{
    /// <inheritdoc />
    public partial class specialRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialRequest",
                table: "MenuItem");

            migrationBuilder.AddColumn<string>(
                name: "SpecialRequest",
                table: "Order",
                type: "varchar(250)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SpecialRequest",
                table: "Order");

            migrationBuilder.AddColumn<string>(
                name: "SpecialRequest",
                table: "MenuItem",
                type: "varchar(250)",
                nullable: false,
                defaultValue: "");
        }
    }
}
