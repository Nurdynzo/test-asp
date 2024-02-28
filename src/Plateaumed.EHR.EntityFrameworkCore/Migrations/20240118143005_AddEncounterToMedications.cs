using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToMedications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "Medications",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medications_EncounterId",
                table: "Medications",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_PatientEncounters_EncounterId",
                table: "Medications",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medications_PatientEncounters_EncounterId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_EncounterId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Medications");
        }
    }
}
