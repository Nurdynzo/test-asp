using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateForPatientImplantMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatienImplants_Patients_PatientId",
                table: "PatienImplants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatienImplants",
                table: "PatienImplants");

            migrationBuilder.RenameTable(
                name: "PatienImplants",
                newName: "PatientImplants");

            migrationBuilder.RenameColumn(
                name: "ImplantTypeOrLocation",
                table: "PatientImplants",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "ImplantStillIntact",
                table: "PatientImplants",
                newName: "IsIntact");

            migrationBuilder.RenameIndex(
                name: "IX_PatienImplants_PatientId",
                table: "PatientImplants",
                newName: "IX_PatientImplants_PatientId");

            migrationBuilder.AlterColumn<long>(
                name: "PatientId",
                table: "PatientImplants",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatientImplants",
                table: "PatientImplants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientImplants_Patients_PatientId",
                table: "PatientImplants",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientImplants_Patients_PatientId",
                table: "PatientImplants");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PatientImplants",
                table: "PatientImplants");

            migrationBuilder.RenameTable(
                name: "PatientImplants",
                newName: "PatienImplants");

            migrationBuilder.RenameColumn(
                name: "Location",
                table: "PatienImplants",
                newName: "ImplantTypeOrLocation");

            migrationBuilder.RenameColumn(
                name: "IsIntact",
                table: "PatienImplants",
                newName: "ImplantStillIntact");

            migrationBuilder.RenameIndex(
                name: "IX_PatientImplants_PatientId",
                table: "PatienImplants",
                newName: "IX_PatienImplants_PatientId");

            migrationBuilder.AlterColumn<long>(
                name: "PatientId",
                table: "PatienImplants",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PatienImplants",
                table: "PatienImplants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatienImplants_Patients_PatientId",
                table: "PatienImplants",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id");
        }
    }
}
