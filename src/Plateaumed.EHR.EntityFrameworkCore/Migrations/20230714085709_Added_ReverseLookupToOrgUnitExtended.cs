using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedReverseLookupToOrgUnitExtended : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitExtendedId",
                table: "AppUserOrganizationUnits",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitExtendedId",
                table: "AppOrganizationUnitRoles",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserOrganizationUnits_OrganizationUnitExtendedId",
                table: "AppUserOrganizationUnits",
                column: "OrganizationUnitExtendedId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnitRoles_OrganizationUnitExtendedId",
                table: "AppOrganizationUnitRoles",
                column: "OrganizationUnitExtendedId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrganizationUnitRoles_AppOrganizationUnitsExtended_Organ~",
                table: "AppOrganizationUnitRoles",
                column: "OrganizationUnitExtendedId",
                principalTable: "AppOrganizationUnitsExtended",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserOrganizationUnits_AppOrganizationUnitsExtended_Organ~",
                table: "AppUserOrganizationUnits",
                column: "OrganizationUnitExtendedId",
                principalTable: "AppOrganizationUnitsExtended",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnitRoles_AppOrganizationUnitsExtended_Organ~",
                table: "AppOrganizationUnitRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUserOrganizationUnits_AppOrganizationUnitsExtended_Organ~",
                table: "AppUserOrganizationUnits");

            migrationBuilder.DropIndex(
                name: "IX_AppUserOrganizationUnits_OrganizationUnitExtendedId",
                table: "AppUserOrganizationUnits");

            migrationBuilder.DropIndex(
                name: "IX_AppOrganizationUnitRoles_OrganizationUnitExtendedId",
                table: "AppOrganizationUnitRoles");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitExtendedId",
                table: "AppUserOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitExtendedId",
                table: "AppOrganizationUnitRoles");
        }
    }
}
