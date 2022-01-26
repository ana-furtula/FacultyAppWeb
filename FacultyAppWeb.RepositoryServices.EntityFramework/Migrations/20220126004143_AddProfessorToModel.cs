using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyAppWeb.RepositoryServices.EntityFramework.Migrations
{
    public partial class AddProfessorToModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProfessorId",
                table: "Lectures",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "ProfessorId",
                table: "ExamRegistrations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    JMBG = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "ebf2d22f-ce31-4234-b3a2-5e1d6eaacf52");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "50b11f12-eda4-48b6-8bcd-289accc03308");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "2f8aa642-94cb-4245-88b7-b693481183dc");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "3e80b58a-f3fd-400b-a16a-f4c3e34b5fc8", "033be1b8-036a-437f-a026-b663c3e22a0b" });

            migrationBuilder.CreateIndex(
                name: "IX_Lectures_ProfessorId",
                table: "Lectures",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_ExamRegistrations_ProfessorId",
                table: "ExamRegistrations",
                column: "ProfessorId");

            migrationBuilder.CreateIndex(
                name: "IX_Professors_Email",
                table: "Professors",
                column: "Email",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ExamRegistrations_Professors_ProfessorId",
                table: "ExamRegistrations",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Lectures_Professors_ProfessorId",
                table: "Lectures",
                column: "ProfessorId",
                principalTable: "Professors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExamRegistrations_Professors_ProfessorId",
                table: "ExamRegistrations");

            migrationBuilder.DropForeignKey(
                name: "FK_Lectures_Professors_ProfessorId",
                table: "Lectures");

            migrationBuilder.DropTable(
                name: "Professors");

            migrationBuilder.DropIndex(
                name: "IX_Lectures_ProfessorId",
                table: "Lectures");

            migrationBuilder.DropIndex(
                name: "IX_ExamRegistrations_ProfessorId",
                table: "ExamRegistrations");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "Lectures");

            migrationBuilder.DropColumn(
                name: "ProfessorId",
                table: "ExamRegistrations");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "84a9337f-8745-4bd9-9b2f-6e30d3bc35b7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "871ead4a-4c4f-4a35-ad58-11a950f44847");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "ca7362cc-ff6d-47af-a605-be84ec882011");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "ad0fb503-169b-4a8c-9145-e2632f772e2d", "18965b7a-129e-45ba-9494-d08e87cf95d0" });
        }
    }
}
