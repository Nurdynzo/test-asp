using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedOrganizationUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AppOrganizationUnits",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FacilityGroupId",
                table: "AppOrganizationUnits",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FacilityId",
                table: "AppOrganizationUnits",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "AppOrganizationUnits",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnits_FacilityGroupId",
                table: "AppOrganizationUnits",
                column: "FacilityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnits_FacilityId",
                table: "AppOrganizationUnits",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrganizationUnits_Facilities_FacilityId",
                table: "AppOrganizationUnits",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrganizationUnits_FacilityGroups_FacilityGroupId",
                table: "AppOrganizationUnits",
                column: "FacilityGroupId",
                principalTable: "FacilityGroups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnits_Facilities_FacilityId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnits_FacilityGroups_FacilityGroupId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropIndex(
                name: "IX_AppOrganizationUnits_FacilityGroupId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropIndex(
                name: "IX_AppOrganizationUnits_FacilityId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AppOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "FacilityGroupId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "AppOrganizationUnits");
        }
    }
}
