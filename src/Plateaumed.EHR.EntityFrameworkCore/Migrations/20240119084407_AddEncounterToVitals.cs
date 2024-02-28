using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToVitals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "PatientVitals",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_EncounterId",
                table: "PatientVitals",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientVitals_PatientEncounters_EncounterId",
                table: "PatientVitals",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientVitals_PatientEncounters_EncounterId",
                table: "PatientVitals");

            migrationBuilder.DropIndex(
                name: "IX_PatientVitals_EncounterId",
                table: "PatientVitals");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "PatientVitals");
        }
    }
}
