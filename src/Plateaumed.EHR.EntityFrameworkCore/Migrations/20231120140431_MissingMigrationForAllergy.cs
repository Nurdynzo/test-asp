using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class MissingMigrationForAllergy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreadtedBy",
                table: "PatientAllergies",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Interval",
                table: "PatientAllergies",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreadtedBy",
                table: "PatientAllergies");

            migrationBuilder.DropColumn(
                name: "Interval",
                table: "PatientAllergies");
        }
    }
}
