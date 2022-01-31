using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FacultyAppWeb.RepositoryServices.EntityFramework.Migrations
{
    public partial class Unique_Constraints_Index_JMBG : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Index",
                table: "Students",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3764c4b0-fc30-480a-87a7-e21eb3cefbd6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "5a7c9f89-b78d-4533-84cd-4caeef054f62");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "d057c2f5-bc45-41b2-9b54-fa8141668b1c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "0e6a5f40-c8e1-455e-ab94-7e5f0d0cbd07", "4114e12b-4e9b-42e9-a26b-9dce4c4a32b2" });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Index",
                table: "Students",
                column: "Index",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_JMBG",
                table: "Students",
                column: "JMBG",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_Index",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_JMBG",
                table: "Students");

            migrationBuilder.AlterColumn<string>(
                name: "Index",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e23e21e7-c931-451a-ad77-aadf801e1d01");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "cbb85655-dd3d-467c-8e4c-5ab4076cd76d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "f448d782-38dc-43df-90c1-3678cdbf95c6");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "SecurityStamp" },
                values: new object[] { "b2c4c62f-d47e-43c0-baed-a8fb6f88c628", "f797a178-ff54-4a92-922f-b36b2fe64cd6" });
        }
    }
}
