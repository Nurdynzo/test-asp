using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTenantEntityWithOnboardingFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category",
                table: "AppTenants",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "HasSignedAgreement",
                table: "AppTenants",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "IndividualGraduatingSchool",
                table: "AppTenants",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndividualGraduatingYear",
                table: "AppTenants",
                type: "character varying(4)",
                maxLength: 4,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IndividualSpecialization",
                table: "AppTenants",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "AppTenants",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Category",
                table: "AppTenants");

            migrationBuilder.DropColumn(
                name: "HasSignedAgreement",
                table: "AppTenants");

            migrationBuilder.DropColumn(
                name: "IndividualGraduatingSchool",
                table: "AppTenants");

            migrationBuilder.DropColumn(
                name: "IndividualGraduatingYear",
                table: "AppTenants");

            migrationBuilder.DropColumn(
                name: "IndividualSpecialization",
                table: "AppTenants");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppTenants");
        }
    }
}
