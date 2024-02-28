using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultFacilityIdToStaffMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DefaultFacilityId",
                table: "StaffMembers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_DefaultFacilityId",
                table: "StaffMembers",
                column: "DefaultFacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_Facilities_DefaultFacilityId",
                table: "StaffMembers",
                column: "DefaultFacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_Facilities_DefaultFacilityId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_StaffMembers_DefaultFacilityId",
                table: "StaffMembers");

            migrationBuilder.DropColumn(
                name: "DefaultFacilityId",
                table: "StaffMembers");
        }
    }
}
