using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResumeModuleApp.Migrations
{
    public partial class revertPosition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "Resumes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Resumes");
        }
    }
}
