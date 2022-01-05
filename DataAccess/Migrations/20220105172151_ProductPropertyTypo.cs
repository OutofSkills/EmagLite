using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESTApi.DataAccess.Migrations
{
    public partial class ProductPropertyTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GarantyDays",
                table: "Products",
                newName: "GuaranteeDays");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GuaranteeDays",
                table: "Products",
                newName: "GarantyDays");
        }
    }
}
