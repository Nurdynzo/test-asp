using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifiedVitalSign : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "PatientVitals");

            migrationBuilder.AddColumn<int>(
                name: "VitalPosition",
                table: "PatientVitals",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VitalPosition",
                table: "PatientVitals");

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "PatientVitals",
                type: "varchar(10)",
                nullable: true);
        }
    }
}
