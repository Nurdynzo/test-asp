using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Misc;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientNextAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientNextAppointments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    UnitId = table.Column<long>(type: "bigint", nullable: false),
                    DateType = table.Column<int>(type: "integer", nullable: false),
                    SeenIn = table.Column<int>(type: "integer", nullable: true),
                    IsToSeeSameDoctor = table.Column<bool>(type: "boolean", nullable: true),
                    DayOfTheWeek = table.Column<DaysOfTheWeek>(type: "days_of_the_week", nullable: true),
                    AppointmentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AppointmentId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_PatientNextAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientNextAppointments_AppOrganizationUnits_UserId",
                        column: x => x.UserId,
                        principalTable: "AppOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientNextAppointments_AppUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientNextAppointments_PatientAppointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "PatientAppointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientNextAppointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientNextAppointments_AppointmentId",
                table: "PatientNextAppointments",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNextAppointments_CreatorUserId",
                table: "PatientNextAppointments",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNextAppointments_PatientId",
                table: "PatientNextAppointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNextAppointments_TenantId_PatientId_UserId",
                table: "PatientNextAppointments",
                columns: new[] { "TenantId", "PatientId", "UserId" });

            migrationBuilder.CreateIndex(
                name: "IX_PatientNextAppointments_UserId",
                table: "PatientNextAppointments",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientNextAppointments");
        }
    }
}
