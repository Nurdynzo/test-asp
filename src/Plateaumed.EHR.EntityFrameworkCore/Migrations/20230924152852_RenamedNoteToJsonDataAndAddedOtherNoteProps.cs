using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RenamedNoteToJsonDataAndAddedOtherNoteProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Note",
                table: "Symptoms",
                newName: "JsonData");

            migrationBuilder.AddColumn<string>(
                name: "OtherNote",
                table: "Symptoms",
                type: "varchar(2000)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherNote",
                table: "Symptoms");

            migrationBuilder.RenameColumn(
                name: "JsonData",
                table: "Symptoms",
                newName: "Note");
        }
    }
}
