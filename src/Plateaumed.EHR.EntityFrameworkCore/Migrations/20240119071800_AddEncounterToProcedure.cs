using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "Procedures",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Procedures_EncounterId",
                table: "Procedures",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Procedures_PatientEncounters_EncounterId",
                table: "Procedures",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Procedures_PatientEncounters_EncounterId",
                table: "Procedures");

            migrationBuilder.DropIndex(
                name: "IX_Procedures_EncounterId",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Procedures");
        }
    }
}
