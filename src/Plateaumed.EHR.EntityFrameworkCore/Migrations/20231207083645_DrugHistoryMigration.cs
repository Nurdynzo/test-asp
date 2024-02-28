using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Misc;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class DrugHistoryMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DrugHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientOnMedication = table.Column<bool>(type: "boolean", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    MedicationName = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Route = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Dose = table.Column<int>(type: "integer", nullable: false),
                    PrescriptionFrequency = table.Column<int>(type: "integer", nullable: false),
                    PrescriptionInterval = table.Column<UnitOfTime>(type: "unit_of_time", nullable: false),
                    CompliantWithMedication = table.Column<bool>(type: "boolean", nullable: false),
                    UsageFrequency = table.Column<int>(type: "integer", nullable: false),
                    UsageInterval = table.Column<UnitOfTime>(type: "unit_of_time", nullable: false),
                    IsMedicationStillBeingTaken = table.Column<bool>(type: "boolean", nullable: false),
                    WhenMedicationStopped = table.Column<int>(type: "integer", nullable: false),
                    StopInterval = table.Column<UnitOfTime>(type: "unit_of_time", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_DrugHistories", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DrugHistories");
        }
    }
}
