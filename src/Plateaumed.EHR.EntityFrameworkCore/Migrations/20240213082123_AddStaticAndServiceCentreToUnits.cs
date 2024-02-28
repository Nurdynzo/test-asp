using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Misc;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddStaticAndServiceCentreToUnits : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsStatic",
                table: "AppOrganizationUnitsExtended",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<ServiceCentreType>(
                name: "ServiceCentre",
                table: "AppOrganizationUnitsExtended",
                type: "service_centre_type",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsStatic",
                table: "AppOrganizationUnitsExtended");

            migrationBuilder.DropColumn(
                name: "ServiceCentre",
                table: "AppOrganizationUnitsExtended");
        }
    }
}
