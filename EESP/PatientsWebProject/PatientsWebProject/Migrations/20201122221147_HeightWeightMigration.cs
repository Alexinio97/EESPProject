using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientsWebProject.Migrations
{
    public partial class HeightWeightMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<float>(
                name: "Height",
                table: "Patients",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Patients",
                type: "real",
                nullable: false,
                defaultValue: 0f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Patients");
        }
    }
}
