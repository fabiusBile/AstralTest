using Microsoft.EntityFrameworkCore.Migrations;

namespace Database.Migrations
{
    public partial class AddEmploymentType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_EmploymentType_TypeId",
                table: "Vacancies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmploymentType",
                table: "EmploymentType");

            migrationBuilder.RenameTable(
                name: "EmploymentType",
                newName: "EmploymentTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmploymentTypes",
                table: "EmploymentTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_EmploymentTypes_TypeId",
                table: "Vacancies",
                column: "TypeId",
                principalTable: "EmploymentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vacancies_EmploymentTypes_TypeId",
                table: "Vacancies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmploymentTypes",
                table: "EmploymentTypes");

            migrationBuilder.RenameTable(
                name: "EmploymentTypes",
                newName: "EmploymentType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmploymentType",
                table: "EmploymentType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Vacancies_EmploymentType_TypeId",
                table: "Vacancies",
                column: "TypeId",
                principalTable: "EmploymentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
