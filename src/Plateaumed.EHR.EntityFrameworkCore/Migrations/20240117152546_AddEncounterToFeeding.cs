using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToFeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "Feeding",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feeding_EncounterId",
                table: "Feeding",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feeding_PatientEncounters_EncounterId",
                table: "Feeding",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feeding_PatientEncounters_EncounterId",
                table: "Feeding");

            migrationBuilder.DropIndex(
                name: "IX_Feeding_EncounterId",
                table: "Feeding");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Feeding");
        }
    }
}
