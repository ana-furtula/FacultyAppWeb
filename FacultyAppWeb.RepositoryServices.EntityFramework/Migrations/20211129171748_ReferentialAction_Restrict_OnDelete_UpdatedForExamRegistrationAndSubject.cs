using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyAppWeb.RepositoryServices.SqlServer.Migrations
{
    public partial class ReferentialAction_Restrict_OnDelete_UpdatedForExamRegistrationAndSubject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_ExamRegistrations_Professors_ProfessorId",
            table: "ExamRegistrations");
            migrationBuilder.AddForeignKey(
                name: "FK_ExamRegistrations_Professors_ProfessorId",
                table: "ExamRegistrations",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_ExamRegistrations_Professors_ProfessorId",
            table: "ExamRegistrations");
            migrationBuilder.AddForeignKey(
                name: "FK_ExamRegistrations_Professors_ProfessorId",
                table: "ExamRegistrations",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
