using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RevertMedicationUnitValuesProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoseValue",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "DurationValue",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "FrequencyValue",
                table: "Medications");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoseValue",
                table: "Medications",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DurationValue",
                table: "Medications",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FrequencyValue",
                table: "Medications",
                type: "integer",
                nullable: true);
        }
    }
}
