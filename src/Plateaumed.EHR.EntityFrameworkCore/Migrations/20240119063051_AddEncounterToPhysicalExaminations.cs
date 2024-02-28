using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToPhysicalExaminations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "PatientPhysicalExaminations",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExaminations_EncounterId",
                table: "PatientPhysicalExaminations",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPhysicalExaminations_PatientEncounters_EncounterId",
                table: "PatientPhysicalExaminations",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientPhysicalExaminations_PatientEncounters_EncounterId",
                table: "PatientPhysicalExaminations");

            migrationBuilder.DropIndex(
                name: "IX_PatientPhysicalExaminations_EncounterId",
                table: "PatientPhysicalExaminations");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "PatientPhysicalExaminations");
        }
    }
}
