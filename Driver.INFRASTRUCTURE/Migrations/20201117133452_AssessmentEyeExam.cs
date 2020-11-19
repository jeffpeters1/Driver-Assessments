using Microsoft.EntityFrameworkCore.Migrations;

namespace Driver.INFRASTRUCTURE.Migrations
{
    public partial class AssessmentEyeExam : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassedEyesight",
                table: "Assessments",
                newName: "PassedEyeExam");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PassedEyeExam",
                table: "Assessments",
                newName: "PassedEyesight");
        }
    }
}
