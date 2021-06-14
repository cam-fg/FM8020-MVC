using Microsoft.EntityFrameworkCore.Migrations;

namespace FM8020_MVC.Migrations
{
    public partial class addResponsibilityColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Responsibility",
                table: "Defects",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Responsibility",
                table: "Defects");
        }
    }
}
