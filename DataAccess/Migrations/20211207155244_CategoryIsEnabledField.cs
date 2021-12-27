using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RESTApi.DataAccess.Migrations
{
    public partial class CategoryIsEnabledField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Categories");
        }
    }
}
