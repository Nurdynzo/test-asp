using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvestigationResultAddPatientEncounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "InvestigationResults",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationResults_EncounterId",
                table: "InvestigationResults",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationResults_PatientEncounters_EncounterId",
                table: "InvestigationResults",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationResults_PatientEncounters_EncounterId",
                table: "InvestigationResults");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationResults_EncounterId",
                table: "InvestigationResults");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "InvestigationResults");
        }
    }
}
