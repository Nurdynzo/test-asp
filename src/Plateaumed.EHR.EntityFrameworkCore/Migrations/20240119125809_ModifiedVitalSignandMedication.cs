using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedVitalSignandMedication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProcedureEntryType",
                table: "PatientVitals",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProcedureEntryType",
                table: "Medications",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "ProcedureId",
                table: "Medications",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medications_ProcedureId",
                table: "Medications",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Procedures_ProcedureId",
                table: "Medications",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Procedures_ProcedureId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_ProcedureId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "ProcedureEntryType",
                table: "PatientVitals");

            migrationBuilder.DropColumn(
                name: "ProcedureEntryType",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "Medications");
        }
    }
}
