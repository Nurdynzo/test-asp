using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ProcedureIdAddedToOtherPlanItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ProcedureId",
                table: "PlanItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanItems_ProcedureId",
                table: "PlanItems",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanItems_Procedures_ProcedureId",
                table: "PlanItems",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanItems_Procedures_ProcedureId",
                table: "PlanItems");

            migrationBuilder.DropIndex(
                name: "IX_PlanItems_ProcedureId",
                table: "PlanItems");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "PlanItems");
        }
    }
}
