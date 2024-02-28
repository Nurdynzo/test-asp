using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RemoveFacilityGroupFromOrganizationUnitAndMakeFacilityIdNotNull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnitsExtended_Facilities_FacilityId",
                table: "AppOrganizationUnitsExtended");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnitsExtended_FacilityGroups_FacilityGroupId",
                table: "AppOrganizationUnitsExtended");

            migrationBuilder.DropIndex(
                name: "IX_AppOrganizationUnitsExtended_FacilityGroupId",
                table: "AppOrganizationUnitsExtended");

            migrationBuilder.DropColumn(
                name: "FacilityGroupId",
                table: "AppOrganizationUnitsExtended");

            migrationBuilder.AlterColumn<long>(
                name: "FacilityId",
                table: "AppOrganizationUnitsExtended",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrganizationUnitsExtended_Facilities_FacilityId",
                table: "AppOrganizationUnitsExtended",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnitsExtended_Facilities_FacilityId",
                table: "AppOrganizationUnitsExtended");

            migrationBuilder.AlterColumn<long>(
                name: "FacilityId",
                table: "AppOrganizationUnitsExtended",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "FacilityGroupId",
                table: "AppOrganizationUnitsExtended",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnitsExtended_FacilityGroupId",
                table: "AppOrganizationUnitsExtended",
                column: "FacilityGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrganizationUnitsExtended_Facilities_FacilityId",
                table: "AppOrganizationUnitsExtended",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrganizationUnitsExtended_FacilityGroups_FacilityGroupId",
                table: "AppOrganizationUnitsExtended",
                column: "FacilityGroupId",
                principalTable: "FacilityGroups",
                principalColumn: "Id");
        }
    }
}
