using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrganizationUnitInheritanceStrategyToTPT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnits_Facilities_FacilityId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnits_FacilityGroups_FacilityGroupId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnitTimes_AppOrganizationUnits_OrganizationU~",
                table: "AppOrganizationUnitTimes");

            migrationBuilder.DropIndex(
                name: "IX_AppOrganizationUnits_FacilityGroupId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropIndex(
                name: "IX_AppOrganizationUnits_FacilityId",
                table: "AppOrganizationUnits");

            migrationBuilder.DropIndex(
                name: "IX_AppOrganizationUnits_TenantId",
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

            migrationBuilder.DropColumn(
                name: "ShortName",
                table: "AppOrganizationUnits");

            migrationBuilder.CreateTable(
                name: "AppOrganizationUnitsExtended",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ShortName = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    FacilityGroupId = table.Column<long>(type: "bigint", nullable: true),
                    FacilityId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppOrganizationUnitsExtended", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppOrganizationUnitsExtended_AppOrganizationUnits_Id",
                        column: x => x.Id,
                        principalTable: "AppOrganizationUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppOrganizationUnitsExtended_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppOrganizationUnitsExtended_FacilityGroups_FacilityGroupId",
                        column: x => x.FacilityGroupId,
                        principalTable: "FacilityGroups",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnitsExtended_FacilityGroupId",
                table: "AppOrganizationUnitsExtended",
                column: "FacilityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnitsExtended_FacilityId",
                table: "AppOrganizationUnitsExtended",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrganizationUnitTimes_AppOrganizationUnitsExtended_Organ~",
                table: "AppOrganizationUnitTimes",
                column: "OrganizationUnitExtendedId",
                principalTable: "AppOrganizationUnitsExtended",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppOrganizationUnitTimes_AppOrganizationUnitsExtended_Organ~",
                table: "AppOrganizationUnitTimes");

            migrationBuilder.DropTable(
                name: "AppOrganizationUnitsExtended");

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

            migrationBuilder.AddColumn<string>(
                name: "ShortName",
                table: "AppOrganizationUnits",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnits_FacilityGroupId",
                table: "AppOrganizationUnits",
                column: "FacilityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnits_FacilityId",
                table: "AppOrganizationUnits",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_AppOrganizationUnits_TenantId",
                table: "AppOrganizationUnits",
                column: "TenantId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_AppOrganizationUnitTimes_AppOrganizationUnits_OrganizationU~",
                table: "AppOrganizationUnitTimes",
                column: "OrganizationUnitExtendedId",
                principalTable: "AppOrganizationUnits",
                principalColumn: "Id");
        }
    }
}
