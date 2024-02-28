using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatientVitalEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientVitals",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    PainScale = table.Column<int>(type: "integer", nullable: false),
                    VitalSignId = table.Column<long>(type: "bigint", nullable: false),
                    MeasurementSiteId = table.Column<long>(type: "bigint", nullable: true),
                    MeasurementRangeId = table.Column<long>(type: "bigint", nullable: true),
                    VitalReading = table.Column<string>(type: "varchar(100)", nullable: true),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_PatientVitals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientVitals_MeasurementRange_MeasurementRangeId",
                        column: x => x.MeasurementRangeId,
                        principalTable: "MeasurementRange",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientVitals_MeasurementSite_MeasurementSiteId",
                        column: x => x.MeasurementSiteId,
                        principalTable: "MeasurementSite",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientVitals_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientVitals_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientVitals_VitalSigns_VitalSignId",
                        column: x => x.VitalSignId,
                        principalTable: "VitalSigns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_MeasurementRangeId",
                table: "PatientVitals",
                column: "MeasurementRangeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_MeasurementSiteId",
                table: "PatientVitals",
                column: "MeasurementSiteId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_PatientId",
                table: "PatientVitals",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_ProcedureId",
                table: "PatientVitals",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_VitalSignId",
                table: "PatientVitals",
                column: "VitalSignId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientVitals");
        }
    }
}
