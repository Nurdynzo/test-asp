using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedVaccineRelationships : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.CreateIndex(
                name: "IX_VaccineHistories_VaccineId",
                table: "VaccineHistories",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_VaccineId",
                table: "Vaccinations",
                column: "VaccineId");

            migrationBuilder.CreateIndex(
                name: "IX_Vaccinations_VaccineScheduleId",
                table: "Vaccinations",
                column: "VaccineScheduleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccinations_VaccineSchedules_VaccineScheduleId",
                table: "Vaccinations",
                column: "VaccineScheduleId",
                principalTable: "VaccineSchedules",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Vaccinations_Vaccines_VaccineId",
                table: "Vaccinations",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_VaccineHistories_Vaccines_VaccineId",
                table: "VaccineHistories",
                column: "VaccineId",
                principalTable: "Vaccines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vaccinations_VaccineSchedules_VaccineScheduleId",
                table: "Vaccinations");

            migrationBuilder.DropForeignKey(
                name: "FK_Vaccinations_Vaccines_VaccineId",
                table: "Vaccinations");

            migrationBuilder.DropForeignKey(
                name: "FK_VaccineHistories_Vaccines_VaccineId",
                table: "VaccineHistories");

            migrationBuilder.DropIndex(
                name: "IX_VaccineHistories_VaccineId",
                table: "VaccineHistories");

            migrationBuilder.DropIndex(
                name: "IX_Vaccinations_VaccineId",
                table: "Vaccinations");

            migrationBuilder.DropIndex(
                name: "IX_Vaccinations_VaccineScheduleId",
                table: "Vaccinations");
        }
    }
}
