using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceAddEncounterId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PatientEncounterId",
                table: "Invoices",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_PatientEncounterId",
                table: "Invoices",
                column: "PatientEncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_PatientEncounters_PatientEncounterId",
                table: "Invoices",
                column: "PatientEncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_PatientEncounters_PatientEncounterId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_PatientEncounterId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "PatientEncounterId",
                table: "Invoices");
        }
    }
}
