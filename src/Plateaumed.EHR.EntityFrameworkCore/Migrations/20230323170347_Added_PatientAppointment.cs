using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Patients;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatientAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientAppointments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    Title = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: true),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsRepeat = table.Column<bool>(type: "boolean", nullable: false),
                    Notes = table.Column<string>(type: "character varying(1024)", maxLength: 1024, nullable: true),
                    RepeatType = table.Column<AppointmentRepeatType>(type: "appointment_repeat_type", nullable: false),
                    Status = table.Column<AppointmentStatusType>(type: "appointment_status_type", nullable: false),
                    Type = table.Column<AppointmentType>(type: "appointment_type", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    PatientReferralId = table.Column<long>(type: "bigint", nullable: true),
                    AttendingPhysician = table.Column<long>(type: "bigint", nullable: true),
                    ReferringClinic = table.Column<long>(type: "bigint", nullable: true),
                    AttendingClinic = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_PatientAppointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientAppointments_AppOrganizationUnits_AttendingClinic",
                        column: x => x.AttendingClinic,
                        principalTable: "AppOrganizationUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientAppointments_AppOrganizationUnits_ReferringClinic",
                        column: x => x.ReferringClinic,
                        principalTable: "AppOrganizationUnits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientAppointments_PatientReferral_PatientReferralId",
                        column: x => x.PatientReferralId,
                        principalTable: "PatientReferral",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientAppointments_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAppointments_StaffMembers_AttendingPhysician",
                        column: x => x.AttendingPhysician,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointments_AttendingClinic",
                table: "PatientAppointments",
                column: "AttendingClinic");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointments_AttendingPhysician",
                table: "PatientAppointments",
                column: "AttendingPhysician");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointments_PatientId",
                table: "PatientAppointments",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointments_PatientReferralId",
                table: "PatientAppointments",
                column: "PatientReferralId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointments_ReferringClinic",
                table: "PatientAppointments",
                column: "ReferringClinic");

            migrationBuilder.CreateIndex(
                name: "IX_PatientAppointments_TenantId",
                table: "PatientAppointments",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientAppointments");
        }
    }
}
