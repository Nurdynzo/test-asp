using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddJobRoleToJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobRoleId",
                table: "Jobs",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobRoleId",
                table: "Jobs",
                column: "JobRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jobs_AppRoles_JobRoleId",
                table: "Jobs",
                column: "JobRoleId",
                principalTable: "AppRoles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jobs_AppRoles_JobRoleId",
                table: "Jobs");

            migrationBuilder.DropIndex(
                name: "IX_Jobs_JobRoleId",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "JobRoleId",
                table: "Jobs");
        }
    }
}
