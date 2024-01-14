using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeModuleApp.Migrations
{
    public partial class removeColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Resumes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
