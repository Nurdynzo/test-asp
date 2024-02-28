using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddRolesToJobs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminRoleId",
                table: "StaffMembers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TeamRoleId",
                table: "Jobs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_AdminRoleId",
                table: "StaffMembers",
                column: "AdminRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_TeamRoleId",
                table: "Jobs",
                column: "TeamRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AppRoles_TeamRoleId",
                table: "Jobs",
                column: "TeamRoleId",
                principalTable: "AppRoles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_AppRoles_AdminRoleId",
                table: "StaffMembers",
                column: "AdminRoleId",
                principalTable: "AppRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AppRoles_TeamRoleId",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_AppRoles_AdminRoleId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_StaffMembers_AdminRoleId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_TeamRoleId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "AdminRoleId",
                table: "StaffMembers");

            migrationBuilder.DropColumn(
                name: "TeamRoleId",
                table: "Jobs");
        }
    }
}
