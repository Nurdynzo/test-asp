using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToDischarge : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "Discharges",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Discharges_EncounterId",
                table: "Discharges",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Discharges_PatientEncounters_EncounterId",
                table: "Discharges",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Discharges_PatientEncounters_EncounterId",
                table: "Discharges");

            migrationBuilder.DropIndex(
                name: "IX_Discharges_EncounterId",
                table: "Discharges");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Discharges");
        }
    }
}
