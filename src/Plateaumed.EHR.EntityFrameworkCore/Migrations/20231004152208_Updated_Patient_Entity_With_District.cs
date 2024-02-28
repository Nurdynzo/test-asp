using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPatientEntityWithDistrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DistrictId",
                table: "Patients",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_DistrictId",
                table: "Patients",
                column: "DistrictId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Districts_DistrictId",
                table: "Patients",
                column: "DistrictId",
                principalTable: "Districts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Districts_DistrictId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_DistrictId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "DistrictId",
                table: "Patients");
        }
    }
}
