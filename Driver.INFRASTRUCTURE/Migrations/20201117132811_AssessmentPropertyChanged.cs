using Microsoft.EntityFrameworkCore.Migrations;

namespace Driver.INFRASTRUCTURE.Migrations
{
    public partial class AssessmentPropertyChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarefulDriver",
                table: "Assessments",
                newName: "PassedDriving");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassedDriving",
                table: "Assessments",
                newName: "CarefulDriver");
        }
    }
}
