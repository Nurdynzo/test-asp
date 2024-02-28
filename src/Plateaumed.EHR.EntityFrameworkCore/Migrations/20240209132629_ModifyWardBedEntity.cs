using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifyWardBedEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BedName",
                table: "WardBeds");

            migrationBuilder.AlterColumn<string>(
                name: "BedNumber",
                table: "WardBeds",
                type: "text",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "BedNumber",
                table: "WardBeds",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BedName",
                table: "WardBeds",
                type: "text",
                nullable: true);
        }
    }
}
