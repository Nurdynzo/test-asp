using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class EnabledNullableForInvestigationResultReviewer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationResultReviewer_InvestigationResults_Investigat~",
                table: "InvestigationResultReviewer");

            migrationBuilder.AlterColumn<long>(
                name: "InvestigationResultId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "ElectroRadPulmInvestigationResultId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InvestigationResultReviewer_ElectroRadPulmInvestigationResu~",
                table: "InvestigationResultReviewer",
                column: "ElectroRadPulmInvestigationResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationResultReviewer_ElectroRadPulmInvestigationResu~",
                table: "InvestigationResultReviewer",
                column: "ElectroRadPulmInvestigationResultId",
                principalTable: "ElectroRadPulmInvestigationResult",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationResultReviewer_InvestigationResults_Investigat~",
                table: "InvestigationResultReviewer",
                column: "InvestigationResultId",
                principalTable: "InvestigationResults",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationResultReviewer_ElectroRadPulmInvestigationResu~",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationResultReviewer_InvestigationResults_Investigat~",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropIndex(
                name: "IX_InvestigationResultReviewer_ElectroRadPulmInvestigationResu~",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropColumn(
                name: "ElectroRadPulmInvestigationResultId",
                table: "InvestigationResultReviewer");

            migrationBuilder.AlterColumn<long>(
                name: "InvestigationResultId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationResultReviewer_InvestigationResults_Investigat~",
                table: "InvestigationResultReviewer",
                column: "InvestigationResultId",
                principalTable: "InvestigationResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
