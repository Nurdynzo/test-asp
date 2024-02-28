using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RenamedToSymtomSnomedIdAndExtendedNotePropsLenght : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SymptomSctId",
                table: "Symptoms",
                newName: "SymptomSnowmedId");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Symptoms",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(2000)",
                oldMaxLength: 2000,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SymptomSnowmedId",
                table: "Symptoms",
                newName: "SymptomSctId");

            migrationBuilder.AlterColumn<string>(
                name: "Note",
                table: "Symptoms",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
