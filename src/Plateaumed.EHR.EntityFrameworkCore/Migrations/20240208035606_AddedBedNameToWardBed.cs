using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedBedNameToWardBed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "WardBeds",
                newName: "BedNumber");

            migrationBuilder.AddColumn<string>(
                name: "BedName",
                table: "WardBeds",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BedName",
                table: "WardBeds");

            migrationBuilder.RenameColumn(
                name: "BedNumber",
                table: "WardBeds",
                newName: "Count");
        }
    }
}
