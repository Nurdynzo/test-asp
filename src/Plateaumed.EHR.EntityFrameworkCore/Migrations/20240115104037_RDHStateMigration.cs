using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RDHStateMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReviewDetailsHistoryStates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    NoFamilyHistory = table.Column<bool>(type: "boolean", nullable: false),
                    NoPhysicalExerciseHistory = table.Column<bool>(type: "boolean", nullable: false),
                    NoChronicIllness = table.Column<bool>(type: "boolean", nullable: false),
                    NoMajorInjuries = table.Column<bool>(type: "boolean", nullable: false),
                    NoTravelHistory = table.Column<bool>(type: "boolean", nullable: false),
                    NoSurgicalHistory = table.Column<bool>(type: "boolean", nullable: false),
                    NoBloodTransfusionHistory = table.Column<bool>(type: "boolean", nullable: false),
                    NoVaccinationHistory = table.Column<bool>(type: "boolean", nullable: false),
                    NoUseOfContraceptives = table.Column<bool>(type: "boolean", nullable: false),
                    NoGynaecologicalIllness = table.Column<bool>(type: "boolean", nullable: false),
                    NoGynaecologicalSurgery = table.Column<bool>(type: "boolean", nullable: false),
                    NoHistoryOfCervicalScreening = table.Column<bool>(type: "boolean", nullable: false),
                    NeverBeenPregnant = table.Column<bool>(type: "boolean", nullable: false),
                    NoDeliveryDetails = table.Column<bool>(type: "boolean", nullable: false),
                    PatientDoesNotTakeAlcohol = table.Column<bool>(type: "boolean", nullable: false),
                    PatientDoesNotSmoke = table.Column<bool>(type: "boolean", nullable: false),
                    NoUseOfRecreationalDrugs = table.Column<bool>(type: "boolean", nullable: false),
                    NotCurrentlyOnMedication = table.Column<bool>(type: "boolean", nullable: false),
                    NoAllergies = table.Column<bool>(type: "boolean", nullable: false),
                    NoImplant = table.Column<bool>(type: "boolean", nullable: false),
                    LastEditorName = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_ReviewDetailsHistoryStates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReviewDetailsHistoryStates");
        }
    }
}
