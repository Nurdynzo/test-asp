using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class updatePatientNextAppointment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientNextAppointments_AppOrganizationUnits_UserId",
                table: "PatientNextAppointments");

            migrationBuilder.DropIndex(
                name: "IX_PatientNextAppointments_UserId",
                table: "PatientNextAppointments");

            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "PatientNextAppointments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PatientNextAppointments_EncounterId",
                table: "PatientNextAppointments",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNextAppointments_UnitId",
                table: "PatientNextAppointments",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientNextAppointments_AppOrganizationUnitsExtended_UnitId",
                table: "PatientNextAppointments",
                column: "UnitId",
                principalTable: "AppOrganizationUnitsExtended",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientNextAppointments_PatientEncounters_EncounterId",
                table: "PatientNextAppointments",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientNextAppointments_AppOrganizationUnitsExtended_UnitId",
                table: "PatientNextAppointments");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientNextAppointments_PatientEncounters_EncounterId",
                table: "PatientNextAppointments");

            migrationBuilder.DropIndex(
                name: "IX_PatientNextAppointments_EncounterId",
                table: "PatientNextAppointments");

            migrationBuilder.DropIndex(
                name: "IX_PatientNextAppointments_UnitId",
                table: "PatientNextAppointments");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "PatientNextAppointments");

            migrationBuilder.CreateIndex(
                name: "IX_PatientNextAppointments_UserId",
                table: "PatientNextAppointments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientNextAppointments_AppOrganizationUnits_UserId",
                table: "PatientNextAppointments",
                column: "UserId",
                principalTable: "AppOrganizationUnits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
