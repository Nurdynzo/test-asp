using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserRelationshipWithPatientAndStaffMember : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Patients_PatientInfoId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffMemberId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_StaffMembers_TenantId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_Patients_TenantId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_PatientInfoId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_StaffMemberId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "StaffMembers");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientInfoId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "StaffMemberId",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "UserInfoId",
                table: "Patients",
                newName: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_UserId",
                table: "StaffMembers",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserId",
                table: "Patients",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_TenantId",
                table: "AppUsers",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_AppUsers_UserId",
                table: "Patients",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_AppUsers_UserId",
                table: "StaffMembers",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_AppUsers_UserId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_AppUsers_UserId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_StaffMembers_UserId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_Patients_UserId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_TenantId",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Patients",
                newName: "UserInfoId");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "StaffMembers",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Patients",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "PatientInfoId",
                table: "AppUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "StaffMemberId",
                table: "AppUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_TenantId",
                table: "StaffMembers",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_TenantId",
                table: "Patients",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_PatientInfoId",
                table: "AppUsers",
                column: "PatientInfoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_StaffMemberId",
                table: "AppUsers",
                column: "StaffMemberId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Patients_PatientInfoId",
                table: "AppUsers",
                column: "PatientInfoId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffMemberId",
                table: "AppUsers",
                column: "StaffMemberId",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }
    }
}
