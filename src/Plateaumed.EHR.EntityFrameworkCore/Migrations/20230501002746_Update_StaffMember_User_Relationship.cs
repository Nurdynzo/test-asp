using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateStaffMemberUserRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffInfoId",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "StaffInfoId",
                table: "AppUsers",
                newName: "StaffMemberId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_StaffInfoId",
                table: "AppUsers",
                newName: "IX_AppUsers_StaffMemberId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffMemberId",
                table: "AppUsers",
                column: "StaffMemberId",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffMemberId",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "StaffMemberId",
                table: "AppUsers",
                newName: "StaffInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_StaffMemberId",
                table: "AppUsers",
                newName: "IX_AppUsers_StaffInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffInfoId",
                table: "AppUsers",
                column: "StaffInfoId",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }
    }
}
