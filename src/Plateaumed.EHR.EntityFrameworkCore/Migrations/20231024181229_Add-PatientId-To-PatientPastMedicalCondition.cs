using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientIdToPatientPastMedicalCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PatientId",
                table: "PatientPastMedicalConditions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PatientPastMedicalConditions_PatientId",
                table: "PatientPastMedicalConditions",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPastMedicalConditions_Patients_PatientId",
                table: "PatientPastMedicalConditions",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientPastMedicalConditions_Patients_PatientId",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropIndex(
                name: "IX_PatientPastMedicalConditions_PatientId",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "PatientPastMedicalConditions");
        }
    }
}
