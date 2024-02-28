using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedProcedureEntity001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Procedures");

            migrationBuilder.DropColumn(
                name: "IsSpecializedProcedure",
                table: "Procedures");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Procedures",
                type: "varchar(250)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecializedProcedure",
                table: "Procedures",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
