using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedIntakeOutputCharting : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcedureEntryType",
                table: "IntakeOutputChartings",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProcedureId",
                table: "IntakeOutputChartings",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntakeOutputChartings_ProcedureId",
                table: "IntakeOutputChartings",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeOutputChartings_Procedures_ProcedureId",
                table: "IntakeOutputChartings",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeOutputChartings_Procedures_ProcedureId",
                table: "IntakeOutputChartings");

            migrationBuilder.DropIndex(
                name: "IX_IntakeOutputChartings_ProcedureId",
                table: "IntakeOutputChartings");

            migrationBuilder.DropColumn(
                name: "ProcedureEntryType",
                table: "IntakeOutputChartings");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "IntakeOutputChartings");
        }
    }
}
