using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedApointmentToSymtom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitId",
                table: "Symptoms");

            migrationBuilder.AddColumn<long>(
                name: "AppointmentId",
                table: "Symptoms",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_AppointmentId",
                table: "Symptoms",
                column: "AppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_PatientAppointments_AppointmentId",
                table: "Symptoms",
                column: "AppointmentId",
                principalTable: "PatientAppointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_PatientAppointments_AppointmentId",
                table: "Symptoms");

            migrationBuilder.DropIndex(
                name: "IX_Symptoms_AppointmentId",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "AppointmentId",
                table: "Symptoms");

            migrationBuilder.AddColumn<long>(
                name: "VisitId",
                table: "Symptoms",
                type: "bigint",
                nullable: true);
        }
    }
}
