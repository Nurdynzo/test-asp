using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Misc;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class PastMedicationConditionFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "MedicationUsageFrequencyUnit",
                table: "PatientPastMedicalConditionMedications",
                type: "text",
                nullable: true,
                oldClrType: typeof(UnitOfTime),
                oldType: "unit_of_time");

            migrationBuilder.AlterColumn<string>(
                name: "FrequencyUnit",
                table: "PatientPastMedicalConditionMedications",
                type: "text",
                nullable: true,
                oldClrType: typeof(UnitOfTime),
                oldType: "unit_of_time");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<UnitOfTime>(
                name: "MedicationUsageFrequencyUnit",
                table: "PatientPastMedicalConditionMedications",
                type: "unit_of_time",
                nullable: false,
                defaultValue: UnitOfTime.Day,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<UnitOfTime>(
                name: "FrequencyUnit",
                table: "PatientPastMedicalConditionMedications",
                type: "unit_of_time",
                nullable: false,
                defaultValue: UnitOfTime.Day,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
