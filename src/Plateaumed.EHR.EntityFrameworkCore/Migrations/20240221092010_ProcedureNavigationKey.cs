using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ProcedureNavigationKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ElectroRadPulmInvestigationResult_ProcedureId",
                table: "ElectroRadPulmInvestigationResult",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_ElectroRadPulmInvestigationResult_Procedures_ProcedureId",
                table: "ElectroRadPulmInvestigationResult",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ElectroRadPulmInvestigationResult_Procedures_ProcedureId",
                table: "ElectroRadPulmInvestigationResult");

            migrationBuilder.DropIndex(
                name: "IX_ElectroRadPulmInvestigationResult_ProcedureId",
                table: "ElectroRadPulmInvestigationResult");
        }
    }
}
