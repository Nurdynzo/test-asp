using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatientInsurerToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "PatientId",
                table: "PatientInsurers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L
            );

            migrationBuilder.CreateIndex(
                name: "IX_PatientInsurers_PatientId",
                table: "PatientInsurers",
                column: "PatientId"
            );

            migrationBuilder.AddForeignKey(
                name: "FK_PatientInsurers_Patients_PatientId",
                table: "PatientInsurers",
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
                name: "FK_PatientInsurers_Patients_PatientId",
                table: "PatientInsurers"
            );

            migrationBuilder.DropIndex(
                name: "IX_PatientInsurers_PatientId",
                table: "PatientInsurers"
            );

            migrationBuilder.DropColumn(name: "PatientId", table: "PatientInsurers");
        }
    }
}
