using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToVaccinationHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "VaccineHistories",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VaccineHistories_EncounterId",
                table: "VaccineHistories",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineHistories_PatientEncounters_EncounterId",
                table: "VaccineHistories",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VaccineHistories_PatientEncounters_EncounterId",
                table: "VaccineHistories");

            migrationBuilder.DropIndex(
                name: "IX_VaccineHistories_EncounterId",
                table: "VaccineHistories");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "VaccineHistories");
        }
    }
}
