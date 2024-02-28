using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToDiagnosis : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "Diagnosis",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Diagnosis_EncounterId",
                table: "Diagnosis",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnosis_PatientEncounters_EncounterId",
                table: "Diagnosis",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnosis_PatientEncounters_EncounterId",
                table: "Diagnosis");

            migrationBuilder.DropIndex(
                name: "IX_Diagnosis_EncounterId",
                table: "Diagnosis");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Diagnosis");
        }
    }
}
