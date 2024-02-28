using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToIntakeOutput : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "IntakeOutputChartings",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_IntakeOutputChartings_EncounterId",
                table: "IntakeOutputChartings",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_IntakeOutputChartings_PatientEncounters_EncounterId",
                table: "IntakeOutputChartings",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IntakeOutputChartings_PatientEncounters_EncounterId",
                table: "IntakeOutputChartings");

            migrationBuilder.DropIndex(
                name: "IX_IntakeOutputChartings_EncounterId",
                table: "IntakeOutputChartings");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "IntakeOutputChartings");
        }
    }
}
