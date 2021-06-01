using Microsoft.EntityFrameworkCore.Migrations;

namespace FM8020_MVC.Migrations
{
    public partial class AddComponentTypeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DefectType",
                table: "Defects",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DefectType",
                table: "Defects");
        }
    }
}
