using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddProcedureIdsToExistingEndpoints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcedureEntryType",
                table: "Procedures",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcedureEntryType",
                table: "PlanItems",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcedureEntryType",
                table: "PatientPhysicalExaminations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProcedureId",
                table: "PatientPhysicalExaminations",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExaminations_ProcedureId",
                table: "PatientPhysicalExaminations",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPhysicalExaminations_Procedures_ProcedureId",
                table: "PatientPhysicalExaminations",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientPhysicalExaminations_Procedures_ProcedureId",
                table: "PatientPhysicalExaminations");

            migrationBuilder.DropIndex(
                name: "IX_PatientPhysicalExaminations_ProcedureId",
                table: "PatientPhysicalExaminations");

            migrationBuilder.DropColumn(
                name: "ProcedureEntryType",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "ProcedureEntryType",
                table: "PlanItems");

            migrationBuilder.DropColumn(
                name: "ProcedureEntryType",
                table: "PatientPhysicalExaminations");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "PatientPhysicalExaminations");
        }
    }
}
