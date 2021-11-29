using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyAppWeb.RepositoryServices.SqlServer.Migrations
{
    public partial class ExamRegistration_Updated_SetProfessorToBeNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamRegistrations_Professors_ProfessorId",
                table: "ExamRegistrations");

            migrationBuilder.AlterColumn<long>(
                name: "ProfessorId",
                table: "ExamRegistrations",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRegistrations_Professors_ProfessorId",
                table: "ExamRegistrations",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamRegistrations_Professors_ProfessorId",
                table: "ExamRegistrations");

            migrationBuilder.AlterColumn<long>(
                name: "ProfessorId",
                table: "ExamRegistrations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

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
