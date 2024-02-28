using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RestructuredPatientEncounterEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientEncounters_Invoices_InvoiceId",
                table: "PatientEncounters");

            migrationBuilder.DropIndex(
                name: "IX_PatientEncounters_InvoiceId",
                table: "PatientEncounters");

            migrationBuilder.DropColumn(
                name: "InvoiceId",
                table: "PatientEncounters");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "PatientEncounters");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "InvoiceId",
                table: "PatientEncounters",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "PatientEncounters",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientEncounters_InvoiceId",
                table: "PatientEncounters",
                column: "InvoiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientEncounters_Invoices_InvoiceId",
                table: "PatientEncounters",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "Id");
        }
    }
}
