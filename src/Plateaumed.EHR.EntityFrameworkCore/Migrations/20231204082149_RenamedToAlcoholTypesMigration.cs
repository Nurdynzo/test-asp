using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RenamedToAlcoholTypesMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AlcoholType",
                table: "AlcoholType");

            migrationBuilder.RenameTable(
                name: "AlcoholType",
                newName: "AlcoholTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlcoholTypes",
                table: "AlcoholTypes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AlcoholTypes",
                table: "AlcoholTypes");

            migrationBuilder.RenameTable(
                name: "AlcoholTypes",
                newName: "AlcoholType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AlcoholType",
                table: "AlcoholType",
                column: "Id");
        }
    }
}
