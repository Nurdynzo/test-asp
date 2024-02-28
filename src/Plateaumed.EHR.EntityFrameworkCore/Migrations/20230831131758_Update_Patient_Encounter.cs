using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Patients;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePatientEncounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientEncounters_PatientAppointments_AppointmentId",
                table: "PatientEncounters");

            migrationBuilder.AlterColumn<AppointmentStatusType>(
                name: "Status",
                table: "PatientEncounters",
                type: "appointment_status_type",
                nullable: true,
                oldClrType: typeof(AppointmentStatusType),
                oldType: "appointment_status_type");

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentId",
                table: "PatientEncounters",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientEncounters_PatientAppointments_AppointmentId",
                table: "PatientEncounters",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientEncounters_PatientAppointments_AppointmentId",
                table: "PatientEncounters");

            migrationBuilder.AlterColumn<AppointmentStatusType>(
                name: "Status",
                table: "PatientEncounters",
                type: "appointment_status_type",
                nullable: false,
                defaultValue: AppointmentStatusType.Pending,
                oldClrType: typeof(AppointmentStatusType),
                oldType: "appointment_status_type",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "AppointmentId",
                table: "PatientEncounters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientEncounters_PatientAppointments_AppointmentId",
                table: "PatientEncounters",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
