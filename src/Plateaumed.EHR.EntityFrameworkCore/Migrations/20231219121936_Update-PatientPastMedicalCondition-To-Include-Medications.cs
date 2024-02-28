using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Misc;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientPastMedicalConditionToIncludeMedications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FrequencyUnit",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "IsCompliantWithMedication",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "MedicationDose",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "MedicationType",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "MedicationUsageFrequency",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "MedicationUsageFrequencyUnit",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "PrescriptionFrequency",
                table: "PatientPastMedicalConditions");

            migrationBuilder.CreateTable(
                name: "PatientPastMedicalConditionMedications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    MedicationType = table.Column<string>(type: "text", nullable: true),
                    MedicationDose = table.Column<string>(type: "text", nullable: true),
                    PrescriptionFrequency = table.Column<int>(type: "integer", nullable: false),
                    FrequencyUnit = table.Column<UnitOfTime>(type: "unit_of_time", nullable: false),
                    IsCompliantWithMedication = table.Column<bool>(type: "boolean", nullable: false),
                    MedicationUsageFrequency = table.Column<int>(type: "integer", nullable: false),
                    MedicationUsageFrequencyUnit = table.Column<UnitOfTime>(type: "unit_of_time", nullable: false),
                    PatientPastMedicalConditionId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientPastMedicalConditionMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPastMedicalConditionMedications_PatientPastMedicalCo~",
                        column: x => x.PatientPastMedicalConditionId,
                        principalTable: "PatientPastMedicalConditions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientPastMedicalConditionMedications_PatientPastMedicalCo~",
                table: "PatientPastMedicalConditionMedications",
                column: "PatientPastMedicalConditionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientPastMedicalConditionMedications");

            migrationBuilder.AddColumn<UnitOfTime>(
                name: "FrequencyUnit",
                table: "PatientPastMedicalConditions",
                type: "unit_of_time",
                nullable: false,
                defaultValue: UnitOfTime.Day);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompliantWithMedication",
                table: "PatientPastMedicalConditions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MedicationDose",
                table: "PatientPastMedicalConditions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MedicationType",
                table: "PatientPastMedicalConditions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MedicationUsageFrequency",
                table: "PatientPastMedicalConditions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<UnitOfTime>(
                name: "MedicationUsageFrequencyUnit",
                table: "PatientPastMedicalConditions",
                type: "unit_of_time",
                nullable: false,
                defaultValue: UnitOfTime.Day);

            migrationBuilder.AddColumn<int>(
                name: "PrescriptionFrequency",
                table: "PatientPastMedicalConditions",
                type: "integer",
                nullable: false,
                defaultValue: 0);

        }
    }
}
