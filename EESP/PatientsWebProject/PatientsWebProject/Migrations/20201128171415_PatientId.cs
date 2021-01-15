using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientsWebProject.Migrations
{
    public partial class PatientId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Patients_PatientId",
                table: "Consultations");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Consultations",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Patients_PatientId",
                table: "Consultations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Consultations_Patients_PatientId",
                table: "Consultations");

            migrationBuilder.AlterColumn<int>(
                name: "PatientId",
                table: "Consultations",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Consultations_Patients_PatientId",
                table: "Consultations",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
