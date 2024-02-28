using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToInputNotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "InputNotes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InputNotes_EncounterId",
                table: "InputNotes",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_InputNotes_PatientEncounters_EncounterId",
                table: "InputNotes",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InputNotes_PatientEncounters_EncounterId",
                table: "InputNotes");

            migrationBuilder.DropIndex(
                name: "IX_InputNotes_EncounterId",
                table: "InputNotes");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "InputNotes");
        }
    }
}
