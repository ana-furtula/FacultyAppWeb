using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyAppWeb.RepositoryServices.EntityFramework.Migrations
{
    public partial class AddSubjectToModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "SubjectId",
                table: "ExamRegistrations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ESPB = table.Column<int>(type: "int", nullable: false),
                    Semester = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "6b4520da-cdd8-47be-8191-a6639dea11a6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "320b85d3-18bb-428a-8ad6-ce6f5ec50760");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "134f6f28-9eb9-4700-a777-9af1894730a6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "4469e9e1-6dd6-40ab-99b3-d40136528979", "4fa341b0-c563-4d55-ad14-5d7d4f88404c" });

            migrationBuilder.CreateIndex(
                name: "IX_ExamRegistrations_SubjectId",
                table: "ExamRegistrations",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRegistrations_Subjects_SubjectId",
                table: "ExamRegistrations",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamRegistrations_Subjects_SubjectId",
                table: "ExamRegistrations");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropIndex(
                name: "IX_ExamRegistrations_SubjectId",
                table: "ExamRegistrations");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "ExamRegistrations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f1ae91c2-a9e7-400a-96ea-1764ec420e93");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "77f382aa-3029-4fb0-ba3c-54d7b6b67be3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "7bf8c5ab-10c2-4f6d-9484-01b53e31ed1d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "67d629fb-17c7-4b6f-836d-c4d073231fc4", "74d127d8-8a3c-491a-b7b2-11a37a5e4e09" });
        }
    }
}
