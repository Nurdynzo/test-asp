using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToAdmission : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AdmittingEncounterId",
                table: "Admissions",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_AdmittingEncounterId",
                table: "Admissions",
                column: "AdmittingEncounterId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Admissions_PatientEncounters_AdmittingEncounterId",
                table: "Admissions",
                column: "AdmittingEncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Admissions_PatientEncounters_AdmittingEncounterId",
                table: "Admissions");

            migrationBuilder.DropIndex(
                name: "IX_Admissions_AdmittingEncounterId",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "AdmittingEncounterId",
                table: "Admissions");
        }
    }
}
