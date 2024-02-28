using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedPhyExamEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientPhysicalExamSuggestionQuestion_PatientPhysicalExamSu~",
                table: "PatientPhysicalExamSuggestionQuestion");

            migrationBuilder.DropIndex(
                name: "IX_PatientPhysicalExamSuggestionQuestion_PatientPhysicalExamSu~",
                table: "PatientPhysicalExamSuggestionQuestion");

            migrationBuilder.DropColumn(
                name: "PatientPhysicalExamSuggestionAnswerId",
                table: "PatientPhysicalExamSuggestionQuestion");

            migrationBuilder.AddColumn<long>(
                name: "PatientPhysicalExamSuggestionQuestionId",
                table: "PatientPhysicalExamSuggestionAnswer",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExamSuggestionAnswer_PatientPhysicalExamSugg~",
                table: "PatientPhysicalExamSuggestionAnswer",
                column: "PatientPhysicalExamSuggestionQuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPhysicalExamSuggestionAnswer_PatientPhysicalExamSugg~",
                table: "PatientPhysicalExamSuggestionAnswer",
                column: "PatientPhysicalExamSuggestionQuestionId",
                principalTable: "PatientPhysicalExamSuggestionQuestion",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientPhysicalExamSuggestionAnswer_PatientPhysicalExamSugg~",
                table: "PatientPhysicalExamSuggestionAnswer");

            migrationBuilder.DropIndex(
                name: "IX_PatientPhysicalExamSuggestionAnswer_PatientPhysicalExamSugg~",
                table: "PatientPhysicalExamSuggestionAnswer");

            migrationBuilder.DropColumn(
                name: "PatientPhysicalExamSuggestionQuestionId",
                table: "PatientPhysicalExamSuggestionAnswer");

            migrationBuilder.AddColumn<long>(
                name: "PatientPhysicalExamSuggestionAnswerId",
                table: "PatientPhysicalExamSuggestionQuestion",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExamSuggestionQuestion_PatientPhysicalExamSu~",
                table: "PatientPhysicalExamSuggestionQuestion",
                column: "PatientPhysicalExamSuggestionAnswerId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientPhysicalExamSuggestionQuestion_PatientPhysicalExamSu~",
                table: "PatientPhysicalExamSuggestionQuestion",
                column: "PatientPhysicalExamSuggestionAnswerId",
                principalTable: "PatientPhysicalExamSuggestionAnswer",
                principalColumn: "Id");
        }
    }
}
