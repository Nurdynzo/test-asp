using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifledImageUploadEndpoint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileId",
                table: "ElectroRadPulmInvestigationResultImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "ElectroRadPulmInvestigationResultImages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "ElectroRadPulmInvestigationResultImages",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileId",
                table: "ElectroRadPulmInvestigationResultImages");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "ElectroRadPulmInvestigationResultImages");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "ElectroRadPulmInvestigationResultImages");
        }
    }
}
