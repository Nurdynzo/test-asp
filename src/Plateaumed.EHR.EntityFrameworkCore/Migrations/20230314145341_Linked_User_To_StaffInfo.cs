using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class LinkedUserToStaffInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "StaffMembers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "StaffId",
                table: "AppUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_StaffId",
                table: "AppUsers",
                column: "StaffId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffId",
                table: "AppUsers",
                column: "StaffId",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_StaffId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "StaffMembers");

            migrationBuilder.DropColumn(
                name: "StaffId",
                table: "AppUsers");
        }
    }
}
