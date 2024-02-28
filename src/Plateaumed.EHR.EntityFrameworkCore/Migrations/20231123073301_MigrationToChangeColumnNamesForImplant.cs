using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class MigrationToChangeColumnNamesForImplant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Location",
                table: "PatientImplants",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ImplantNameOrLocation",
                table: "ImplantSuggestions",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "PatientImplants",
                newName: "Location");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ImplantSuggestions",
                newName: "ImplantNameOrLocation");
        }
    }
}
