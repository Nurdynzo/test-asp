using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPatientScanDocumentsEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AssigneeId",
                table: "PatientScanDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "ReviewNote",
                table: "PatientScanDocuments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssigneeId",
                table: "PatientScanDocuments");

            migrationBuilder.DropColumn(
                name: "ReviewNote",
                table: "PatientScanDocuments");
        }
    }
}
