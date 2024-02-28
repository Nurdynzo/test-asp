using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddDiagnosisToInvestigationRequest : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DiagnosisId",
                table: "InvestigationRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationRequests_DiagnosisId",
                table: "InvestigationRequests",
                column: "DiagnosisId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationRequests_Diagnosis_DiagnosisId",
                table: "InvestigationRequests",
                column: "DiagnosisId",
                principalTable: "Diagnosis",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationRequests_Diagnosis_DiagnosisId",
                table: "InvestigationRequests");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationRequests_DiagnosisId",
                table: "InvestigationRequests");

            migrationBuilder.DropColumn(
                name: "DiagnosisId",
                table: "InvestigationRequests");
        }
    }
}
