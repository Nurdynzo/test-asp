using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddEncounterToMeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "EncounterId",
                table: "Meals",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Meals_EncounterId",
                table: "Meals",
                column: "EncounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Meals_PatientEncounters_EncounterId",
                table: "Meals",
                column: "EncounterId",
                principalTable: "PatientEncounters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Meals_PatientEncounters_EncounterId",
                table: "Meals");

            migrationBuilder.DropIndex(
                name: "IX_Meals_EncounterId",
                table: "Meals");

            migrationBuilder.DropColumn(
                name: "EncounterId",
                table: "Meals");
        }
    }
}
