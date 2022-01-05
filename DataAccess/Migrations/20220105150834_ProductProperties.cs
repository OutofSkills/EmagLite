using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESTApi.DataAccess.Migrations
{
    public partial class ProductProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DeliveryPrice",
                table: "Products",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "GarantyDays",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeliveryPrice",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "GarantyDays",
                table: "Products");
        }
    }
}
