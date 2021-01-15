using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PatientsWebProject.Migrations
{
    public partial class NewColumnsMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "BirthDay",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "BirthDay",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Patients");
        }
    }
}
