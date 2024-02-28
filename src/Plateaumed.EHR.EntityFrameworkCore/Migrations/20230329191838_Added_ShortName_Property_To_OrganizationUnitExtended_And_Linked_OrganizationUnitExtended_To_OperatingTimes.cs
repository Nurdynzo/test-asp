using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedShortNamePropertyToOrganizationUnitExtendedAndLinkedOrganizationUnitExtendedToOperatingTimes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnitTimes_AppOrganizationUnits_OrganizationU~",
                table: "AppOrganizationUnitTimes");

            migrationBuilder.DropIndex(
                name: "IX_AppOrganizationUnitTimes_OrganizationUnitId",
                table: "AppOrganizationUnitTimes");

            migrationBuilder.AddColumn<long>(
                name: "OrganizationUnitExtendedId",
                table: "AppOrganizationUnitTimes",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "AppOrganizationUnits",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnitTimes_OrganizationUnitExtendedId",
                table: "AppOrganizationUnitTimes",
                column: "OrganizationUnitExtendedId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnits_TenantId",
                table: "AppOrganizationUnits",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrganizationUnitTimes_AppOrganizationUnits_OrganizationU~",
                table: "AppOrganizationUnitTimes",
                column: "OrganizationUnitExtendedId",
                principalTable: "AppOrganizationUnits",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnitTimes_AppOrganizationUnits_OrganizationU~",
                table: "AppOrganizationUnitTimes");

            migrationBuilder.DropIndex(
                name: "IX_AppOrganizationUnitTimes_OrganizationUnitExtendedId",
                table: "AppOrganizationUnitTimes");

            migrationBuilder.DropIndex(
                name: "IX_AppOrganizationUnits_TenantId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "OrganizationUnitExtendedId",
                table: "AppOrganizationUnitTimes");

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "AppOrganizationUnits");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnitTimes_OrganizationUnitId",
                table: "AppOrganizationUnitTimes",
                column: "OrganizationUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrganizationUnitTimes_AppOrganizationUnits_OrganizationU~",
                table: "AppOrganizationUnitTimes",
                column: "OrganizationUnitId",
                principalTable: "AppOrganizationUnits",
                principalColumn: "Id");
        }
    }
}
