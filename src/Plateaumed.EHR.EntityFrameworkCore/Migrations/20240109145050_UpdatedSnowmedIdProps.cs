using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSnowmedIdProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SymptomSnowmedId",
                table: "PatientPhysicalExamSuggestionAnswer",
                newName: "SnowmedId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SnowmedId",
                table: "PatientPhysicalExamSuggestionAnswer",
                newName: "SymptomSnowmedId");
        }
    }
}
