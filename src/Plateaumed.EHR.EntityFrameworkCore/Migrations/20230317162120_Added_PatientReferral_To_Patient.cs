using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatientReferralToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PatientId",
                table: "PatientReferralLetters",
                type: "bigint",
                nullable: false,
                defaultValue: 0L
            );

            migrationBuilder.CreateIndex(
                name: "IX_PatientReferralLetters_PatientId",
                table: "PatientReferralLetters",
                column: "PatientId",
                unique: true
            );

            migrationBuilder.AddForeignKey(
                name: "FK_PatientReferralLetters_Patients_PatientId",
                table: "PatientReferralLetters",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientReferralLetters_Patients_PatientId",
                table: "PatientReferralLetters"
            );

            migrationBuilder.DropIndex(
                name: "IX_PatientReferralLetters_PatientId",
                table: "PatientReferralLetters"
            );

            migrationBuilder.DropColumn(name: "PatientId", table: "PatientReferralLetters");
        }
    }
}
