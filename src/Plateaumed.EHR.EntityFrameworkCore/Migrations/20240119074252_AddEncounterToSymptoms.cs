using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToSymptoms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "Symptoms",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Symptoms_EncounterId",
                table: "Symptoms",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Symptoms_PatientEncounters_EncounterId",
                table: "Symptoms",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Symptoms_PatientEncounters_EncounterId",
                table: "Symptoms");

            migrationBuilder.DropIndex(
                name: "IX_Symptoms_EncounterId",
                table: "Symptoms");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Symptoms");
        }
    }
}
