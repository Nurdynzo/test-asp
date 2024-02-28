using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToPlanItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "PlanItems",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanItems_EncounterId",
                table: "PlanItems",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanItems_PatientEncounters_EncounterId",
                table: "PlanItems",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanItems_PatientEncounters_EncounterId",
                table: "PlanItems");

            migrationBuilder.DropIndex(
                name: "IX_PlanItems_EncounterId",
                table: "PlanItems");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "PlanItems");
        }
    }
}
