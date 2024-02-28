using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ChangeClinicToExtendedUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_AttendingClinicId",
                table: "PatientAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_ReferringClinicId",
                table: "PatientAppointments");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnitsExtended_AttendingC~",
                table: "PatientAppointments",
                column: "AttendingClinicId",
                principalTable: "AppOrganizationUnitsExtended",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnitsExtended_ReferringC~",
                table: "PatientAppointments",
                column: "ReferringClinicId",
                principalTable: "AppOrganizationUnitsExtended",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnitsExtended_AttendingC~",
                table: "PatientAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnitsExtended_ReferringC~",
                table: "PatientAppointments");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_AttendingClinicId",
                table: "PatientAppointments",
                column: "AttendingClinicId",
                principalTable: "AppOrganizationUnits",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientAppointments_AppOrganizationUnits_ReferringClinicId",
                table: "PatientAppointments",
                column: "ReferringClinicId",
                principalTable: "AppOrganizationUnits",
                principalColumn: "Id");
        }
    }
}
