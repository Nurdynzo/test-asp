using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class FixDipstick : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DipstickInvestigation_Investigations_InvestigationId",
                table: "DipstickInvestigation");

            migrationBuilder.DropForeignKey(
                name: "FK_DipstickInvestigation_Investigations_InvestigationId2",
                table: "DipstickInvestigation");

            migrationBuilder.DropIndex(
                name: "IX_DipstickInvestigation_InvestigationId2",
                table: "DipstickInvestigation");

            migrationBuilder.DropColumn(
                name: "InvestigationId2",
                table: "DipstickInvestigation");

            migrationBuilder.AddForeignKey(
                name: "FK_DipstickInvestigation_Investigations_InvestigationId",
                table: "DipstickInvestigation",
                column: "InvestigationId",
                principalTable: "Investigations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DipstickInvestigation_Investigations_InvestigationId",
                table: "DipstickInvestigation");

            migrationBuilder.AddColumn<long>(
                name: "InvestigationId2",
                table: "DipstickInvestigation",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_DipstickInvestigation_InvestigationId2",
                table: "DipstickInvestigation",
                column: "InvestigationId2");

            migrationBuilder.AddForeignKey(
                name: "FK_DipstickInvestigation_Investigations_InvestigationId",
                table: "DipstickInvestigation",
                column: "InvestigationId",
                principalTable: "Investigations",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DipstickInvestigation_Investigations_InvestigationId2",
                table: "DipstickInvestigation",
                column: "InvestigationId2",
                principalTable: "Investigations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
