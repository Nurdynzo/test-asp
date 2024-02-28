using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddNurseCarePlan : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NursingDiagnosis",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingDiagnosis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NursingEvaluations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingEvaluations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NursingOutcomes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    DiagnosisId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingOutcomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingOutcomes_NursingDiagnosis_DiagnosisId",
                        column: x => x.DiagnosisId,
                        principalTable: "NursingDiagnosis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NursingCareSummaries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    EncounterId = table.Column<long>(type: "bigint", nullable: false),
                    NursingDiagnosisText = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    NursingEvaluationText = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    NursingDiagnosisId = table.Column<long>(type: "bigint", nullable: true),
                    NursingEvaluationId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_NursingCareSummaries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingCareSummaries_NursingDiagnosis_NursingDiagnosisId",
                        column: x => x.NursingDiagnosisId,
                        principalTable: "NursingDiagnosis",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NursingCareSummaries_NursingEvaluations_NursingEvaluationId",
                        column: x => x.NursingEvaluationId,
                        principalTable: "NursingEvaluations",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NursingCareSummaries_PatientEncounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "PatientEncounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NursingCareSummaries_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NursingCareIntervention",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    NursingOutcomesId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingCareIntervention", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingCareIntervention_NursingOutcomes_NursingOutcomesId",
                        column: x => x.NursingOutcomesId,
                        principalTable: "NursingOutcomes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientNursingOutcomes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NursingCareSummaryId = table.Column<long>(type: "bigint", nullable: false),
                    NursingOutcomeId = table.Column<long>(type: "bigint", nullable: true),
                    NursingOutcomesText = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientNursingOutcomes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientNursingOutcomes_NursingCareSummaries_NursingCareSumm~",
                        column: x => x.NursingCareSummaryId,
                        principalTable: "NursingCareSummaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientNursingOutcomes_NursingOutcomes_NursingOutcomeId",
                        column: x => x.NursingOutcomeId,
                        principalTable: "NursingOutcomes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "NursingActivities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    NursingCareInterventionsId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NursingActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NursingActivities_NursingCareIntervention_NursingCareInterv~",
                        column: x => x.NursingCareInterventionsId,
                        principalTable: "NursingCareIntervention",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientNursingCareInterventions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NursingCareSummaryId = table.Column<long>(type: "bigint", nullable: false),
                    NursingCareInterventionsId = table.Column<long>(type: "bigint", nullable: true),
                    NursingCareInterventionsText = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientNursingCareInterventions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientNursingCareInterventions_NursingCareIntervention_Nur~",
                        column: x => x.NursingCareInterventionsId,
                        principalTable: "NursingCareIntervention",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientNursingCareInterventions_NursingCareSummaries_Nursin~",
                        column: x => x.NursingCareSummaryId,
                        principalTable: "NursingCareSummaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientNursingActivities",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NursingCareSummaryId = table.Column<long>(type: "bigint", nullable: false),
                    NursingActivitiesId = table.Column<long>(type: "bigint", nullable: true),
                    NursingActivitiesText = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientNursingActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientNursingActivities_NursingActivities_NursingActivitie~",
                        column: x => x.NursingActivitiesId,
                        principalTable: "NursingActivities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientNursingActivities_NursingCareSummaries_NursingCareSu~",
                        column: x => x.NursingCareSummaryId,
                        principalTable: "NursingCareSummaries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NursingActivities_NursingCareInterventionsId",
                table: "NursingActivities",
                column: "NursingCareInterventionsId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingCareIntervention_NursingOutcomesId",
                table: "NursingCareIntervention",
                column: "NursingOutcomesId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingCareSummaries_EncounterId",
                table: "NursingCareSummaries",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingCareSummaries_NursingDiagnosisId",
                table: "NursingCareSummaries",
                column: "NursingDiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingCareSummaries_NursingEvaluationId",
                table: "NursingCareSummaries",
                column: "NursingEvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingCareSummaries_PatientId",
                table: "NursingCareSummaries",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_NursingOutcomes_DiagnosisId",
                table: "NursingOutcomes",
                column: "DiagnosisId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNursingActivities_NursingActivitiesId",
                table: "PatientNursingActivities",
                column: "NursingActivitiesId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNursingActivities_NursingCareSummaryId",
                table: "PatientNursingActivities",
                column: "NursingCareSummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNursingCareInterventions_NursingCareInterventionsId",
                table: "PatientNursingCareInterventions",
                column: "NursingCareInterventionsId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNursingCareInterventions_NursingCareSummaryId",
                table: "PatientNursingCareInterventions",
                column: "NursingCareSummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNursingOutcomes_NursingCareSummaryId",
                table: "PatientNursingOutcomes",
                column: "NursingCareSummaryId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNursingOutcomes_NursingOutcomeId",
                table: "PatientNursingOutcomes",
                column: "NursingOutcomeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientNursingActivities");

            migrationBuilder.DropTable(
                name: "PatientNursingCareInterventions");

            migrationBuilder.DropTable(
                name: "PatientNursingOutcomes");

            migrationBuilder.DropTable(
                name: "NursingActivities");

            migrationBuilder.DropTable(
                name: "NursingCareSummaries");

            migrationBuilder.DropTable(
                name: "NursingCareIntervention");

            migrationBuilder.DropTable(
                name: "NursingEvaluations");

            migrationBuilder.DropTable(
                name: "NursingOutcomes");

            migrationBuilder.DropTable(
                name: "NursingDiagnosis");
        }
    }
}
