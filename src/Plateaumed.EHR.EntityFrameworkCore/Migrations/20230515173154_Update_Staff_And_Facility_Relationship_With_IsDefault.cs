using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStaffAndFacilityRelationshipWithIsDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_Facilities_DefaultFacilityId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_StaffMembers_DefaultFacilityId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_FacilityStaff_TenantId",
                table: "FacilityStaff");

            migrationBuilder.DropColumn(
                name: "DefaultFacilityId",
                table: "StaffMembers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "FacilityStaff");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "FacilityStaff",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "FacilityStaff");

            migrationBuilder.AddColumn<long>(
                name: "DefaultFacilityId",
                table: "StaffMembers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "FacilityStaff",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_DefaultFacilityId",
                table: "StaffMembers",
                column: "DefaultFacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityStaff_TenantId",
                table: "FacilityStaff",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_Facilities_DefaultFacilityId",
                table: "StaffMembers",
                column: "DefaultFacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }
    }
}
