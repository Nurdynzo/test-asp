using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class includedInvestigationResultIdInInvestigationsComponentEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationComponentResult_InvestigationResults_Investiga~",
                table: "InvestigationComponentResult");

            migrationBuilder.AlterColumn<long>(
                name: "InvestigationResultId",
                table: "InvestigationComponentResult",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationComponentResult_InvestigationResults_Investiga~",
                table: "InvestigationComponentResult",
                column: "InvestigationResultId",
                principalTable: "InvestigationResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationComponentResult_InvestigationResults_Investiga~",
                table: "InvestigationComponentResult");

            migrationBuilder.AlterColumn<long>(
                name: "InvestigationResultId",
                table: "InvestigationComponentResult",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationComponentResult_InvestigationResults_Investiga~",
                table: "InvestigationComponentResult",
                column: "InvestigationResultId",
                principalTable: "InvestigationResults",
                principalColumn: "Id");
        }
    }
}
