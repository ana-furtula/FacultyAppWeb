using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyAppWeb.RepositoryServices.EntityFramework.Migrations
{
    public partial class SeedDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "9a62b53f-7900-41a4-a29e-4fa2d33151ab");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "f0fefca2-155f-4bd7-828c-ec4eefea40a7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "182e1dc3-e01e-4e67-b7fc-7dc8580c8445");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "9d45c5c9-3e47-4357-b8ed-df232d020399", "be8dfb95-90fd-4a3c-b10f-52f8536556a5" });

            migrationBuilder.InsertData(
                table: "Professors",
                columns: new[] { "Id", "Email", "FirstName", "JMBG", "LastName" },
                values: new object[,]
                {
                    { 2L, "marko@gmail.com", "Marko", "1234561234561", "Markovic" },
                    { 3L, "lazar@gmail.com", "Lazar", "1234567123456", "Lazarevic" },
                    { 4L, "ana1@gmail.com", "Ana", "1234567812345", "Anic" },
                    { 5L, "lena@gmail.com", "Lena", "1234567891234", "Lenic" },
                    { 6L, "mika@gmail.com", "Mika", "1234567823456", "Mikic" },
                    { 7L, "gaga@gmail.com", "Dragana", "2345678912345", "Stefanovic" },
                    { 8L, "nikola@gmail.com", "Nikola", "0123456789012", "Nikolic" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Email", "FirstName", "Index", "JMBG", "LastName" },
                values: new object[,]
                {
                    { 2L, "filip1@gmail.com", "Filip", "2014/0155", "2512995280025", "Furtula" },
                    { 3L, "ana1@gmail.com", "Aleksandra", "2018/0175", "0207999285019", "Furtula" },
                    { 10002L, "matija@gmail.com", "Matija", "2012/0001", "1111111111111", "Matijevic" },
                    { 10003L, "milos@gmail.com", "Milos", "2000/2000", "2222222222222", "Milosevic" },
                    { 20002L, "vojin@gmail.com", "Vojin", "1234/2000", "1234567891234", "Vojislavljevic" }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "ESPB", "Name", "Semester" },
                values: new object[,]
                {
                    { 10002L, 4, "EPOS", 4 },
                    { 10004L, 5, "Projektovanje softvera", 5 },
                    { 10005L, 6, "Softverski paterni", 6 },
                    { 20002L, 6, "Programski jezici", 6 },
                    { 20003L, 3, "ITEH", 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Professors_JMBG",
                table: "Professors",
                column: "JMBG",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Professors_JMBG",
                table: "Professors");

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Professors",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 10002L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 10003L);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 20002L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 10002L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 10004L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 10005L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 20002L);

            migrationBuilder.DeleteData(
                table: "Subjects",
                keyColumn: "Id",
                keyValue: 20003L);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "793c6a95-7a7f-4b29-a960-dec262194b68");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "8a55da9a-297f-4cfd-910f-31b9cdf8969a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e2497664-f230-426d-b4d8-7890cc6c3857");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "5439da8e-b3d7-4cc9-b6d7-c5a1a704af0a", "c6c7bdde-942a-4186-932f-0ccac6d9b1bf" });
        }
    }
}
