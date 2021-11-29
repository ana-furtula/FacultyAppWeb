using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyAppWeb.RepositoryServices.SqlServer.Migrations
{
    public partial class ReferentialAction_Restrict_OnDelete_UpdatedForExamRegistrationAndSubjectAndStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_ExamRegistrations_Students_StudentId",
            table: "ExamRegistrations");
            migrationBuilder.AddForeignKey(
                name: "FK_ExamRegistrations_Students_StudentId",
                table: "ExamRegistrations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.DropForeignKey(
            name: "FK_ExamRegistrations_Subjects_SubjectId",
            table: "ExamRegistrations");
            migrationBuilder.AddForeignKey(
                name: "FK_ExamRegistrations_Subjects_SubjectId",
                table: "ExamRegistrations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
            name: "FK_ExamRegistrations_Students_StudentId",
            table: "ExamRegistrations");
            migrationBuilder.AddForeignKey(
                name: "FK_ExamRegistrations_Students_StudentId",
                table: "ExamRegistrations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.DropForeignKey(
            name: "FK_ExamRegistrations_Subjects_SubjectId",
            table: "ExamRegistrations");
            migrationBuilder.AddForeignKey(
                name: "FK_ExamRegistrations_Subjects_SubjectId",
                table: "ExamRegistrations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
