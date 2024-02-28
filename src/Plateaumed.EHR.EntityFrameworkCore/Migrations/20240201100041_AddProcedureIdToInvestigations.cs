using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddProcedureIdToInvestigations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProcedureId",
                table: "InvestigationResults",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProcedureId",
                table: "InvestigationRequests",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationResults_ProcedureId",
                table: "InvestigationResults",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationRequests_ProcedureId",
                table: "InvestigationRequests",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationRequests_Procedures_ProcedureId",
                table: "InvestigationRequests",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationResults_Procedures_ProcedureId",
                table: "InvestigationResults",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationRequests_Procedures_ProcedureId",
                table: "InvestigationRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationResults_Procedures_ProcedureId",
                table: "InvestigationResults");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationResults_ProcedureId",
                table: "InvestigationResults");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationRequests_ProcedureId",
                table: "InvestigationRequests");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "InvestigationResults");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "InvestigationRequests");
        }
    }
}
