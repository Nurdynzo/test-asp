using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedRelationFromEncounterToWardBeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "WardBeds",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WardBeds_EncounterId",
                table: "WardBeds",
                column: "EncounterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_WardBeds_PatientEncounters_EncounterId",
                table: "WardBeds",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WardBeds_PatientEncounters_EncounterId",
                table: "WardBeds");

            migrationBuilder.DropIndex(
                name: "IX_WardBeds_EncounterId",
                table: "WardBeds");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "WardBeds");
        }
    }
}
