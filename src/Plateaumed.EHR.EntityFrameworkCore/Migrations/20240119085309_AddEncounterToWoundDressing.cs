using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToWoundDressing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "WoundDressing",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WoundDressing_EncounterId",
                table: "WoundDressing",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_WoundDressing_PatientEncounters_EncounterId",
                table: "WoundDressing",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WoundDressing_PatientEncounters_EncounterId",
                table: "WoundDressing");

            migrationBuilder.DropIndex(
                name: "IX_WoundDressing_EncounterId",
                table: "WoundDressing");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "WoundDressing");
        }
    }
}
