using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToBedmaking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "BedMaking",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BedMaking_EncounterId",
                table: "BedMaking",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_BedMaking_PatientEncounters_EncounterId",
                table: "BedMaking",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BedMaking_PatientEncounters_EncounterId",
                table: "BedMaking");

            migrationBuilder.DropIndex(
                name: "IX_BedMaking_EncounterId",
                table: "BedMaking");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "BedMaking");
        }
    }
}
