using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddDischargeTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discharges",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: true),
                    IsFinalized = table.Column<bool>(type: "boolean", nullable: false),
                    DischargeType = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "varchar(2000)", nullable: true),
                    status = table.Column<int>(type: "integer", nullable: false),
                    isBroughtInDead = table.Column<bool>(type: "boolean", nullable: false),
                    DateOfDeath = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    TimeOfDeath = table.Column<string>(type: "text", nullable: true),
                    TimeCPRCommenced = table.Column<string>(type: "text", nullable: true),
                    TimeCPREnded = table.Column<string>(type: "text", nullable: true),
                    CausesOfDeath = table.Column<string>(type: "varchar(2000)", nullable: true),
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
                    table.PrimaryKey("PK_Discharges", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discharges_AppUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discharges_PatientAppointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "PatientAppointments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discharges_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DischargeMedications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    DischargeId = table.Column<long>(type: "bigint", nullable: false),
                    MedicationId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_DischargeMedications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DischargeMedications_Discharges_DischargeId",
                        column: x => x.DischargeId,
                        principalTable: "Discharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DischargeMedications_Medications_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DischargePlanItems",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    DischargeId = table.Column<long>(type: "bigint", nullable: false),
                    PlanItemId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_DischargePlanItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DischargePlanItems_Discharges_DischargeId",
                        column: x => x.DischargeId,
                        principalTable: "Discharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DischargePlanItems_PlanItems_PlanItemId",
                        column: x => x.PlanItemId,
                        principalTable: "PlanItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DischargeMedications_DischargeId",
                table: "DischargeMedications",
                column: "DischargeId");

            migrationBuilder.CreateIndex(
                name: "IX_DischargeMedications_MedicationId",
                table: "DischargeMedications",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_DischargeMedications_TenantId_DischargeId_MedicationId",
                table: "DischargeMedications",
                columns: new[] { "TenantId", "DischargeId", "MedicationId" });

            migrationBuilder.CreateIndex(
                name: "IX_DischargePlanItems_DischargeId",
                table: "DischargePlanItems",
                column: "DischargeId");

            migrationBuilder.CreateIndex(
                name: "IX_DischargePlanItems_PlanItemId",
                table: "DischargePlanItems",
                column: "PlanItemId");

            migrationBuilder.CreateIndex(
                name: "IX_DischargePlanItems_TenantId_DischargeId_PlanItemId",
                table: "DischargePlanItems",
                columns: new[] { "TenantId", "DischargeId", "PlanItemId" });

            migrationBuilder.CreateIndex(
                name: "IX_Discharges_AppointmentId",
                table: "Discharges",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Discharges_CreatorUserId",
                table: "Discharges",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Discharges_PatientId",
                table: "Discharges",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Discharges_TenantId_PatientId",
                table: "Discharges",
                columns: new[] { "TenantId", "PatientId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DischargeMedications");

            migrationBuilder.DropTable(
                name: "DischargePlanItems");

            migrationBuilder.DropTable(
                name: "Discharges");
        }
    }
}
