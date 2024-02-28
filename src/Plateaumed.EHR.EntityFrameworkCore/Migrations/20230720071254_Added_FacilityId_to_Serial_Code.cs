using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedFacilityIdtoSerialCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientCodeMapping_Facilities_FacilityId",
                table: "PatientCodeMapping");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientCodeMapping_Patients_PatientId",
                table: "PatientCodeMapping");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientCodeMapping",
                table: "PatientCodeMapping");

            migrationBuilder.RenameTable(
                name: "PatientCodeMapping",
                newName: "PatientCodeMappings");

            migrationBuilder.RenameIndex(
                name: "IX_PatientCodeMapping_PatientId",
                table: "PatientCodeMappings",
                newName: "IX_PatientCodeMappings_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientCodeMapping_FacilityId",
                table: "PatientCodeMappings",
                newName: "IX_PatientCodeMappings_FacilityId");

            migrationBuilder.AddColumn<long>(
                name: "FacilityId",
                table: "SerialCode",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientCodeMappings",
                table: "PatientCodeMappings",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCodeMappings_Facilities_FacilityId",
                table: "PatientCodeMappings",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCodeMappings_Patients_PatientId",
                table: "PatientCodeMappings",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientCodeMappings_Facilities_FacilityId",
                table: "PatientCodeMappings");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientCodeMappings_Patients_PatientId",
                table: "PatientCodeMappings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientCodeMappings",
                table: "PatientCodeMappings");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "SerialCode");

            migrationBuilder.RenameTable(
                name: "PatientCodeMappings",
                newName: "PatientCodeMapping");

            migrationBuilder.RenameIndex(
                name: "IX_PatientCodeMappings_PatientId",
                table: "PatientCodeMapping",
                newName: "IX_PatientCodeMapping_PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientCodeMappings_FacilityId",
                table: "PatientCodeMapping",
                newName: "IX_PatientCodeMapping_FacilityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientCodeMapping",
                table: "PatientCodeMapping",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCodeMapping_Facilities_FacilityId",
                table: "PatientCodeMapping",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCodeMapping_Patients_PatientId",
                table: "PatientCodeMapping",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
