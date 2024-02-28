using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddWardEmergencyToIntervention : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EventId",
                table: "PatientInterventions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientInterventions_EventId",
                table: "PatientInterventions",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientInterventions_WardEmergencies_EventId",
                table: "PatientInterventions",
                column: "EventId",
                principalTable: "WardEmergencies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientInterventions_WardEmergencies_EventId",
                table: "PatientInterventions");

            migrationBuilder.DropIndex(
                name: "IX_PatientInterventions_EventId",
                table: "PatientInterventions");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "PatientInterventions");
        }
    }
}
