using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class LinkedUserToPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffId",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "StaffId",
                table: "AppUsers",
                newName: "StaffInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_StaffId",
                table: "AppUsers",
                newName: "IX_AppUsers_StaffInfoId");

            migrationBuilder.AddColumn<long>(
                name: "UserInfoId",
                table: "Patients",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "PatientInfoId",
                table: "AppUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_PatientInfoId",
                table: "AppUsers",
                column: "PatientInfoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Patients_PatientInfoId",
                table: "AppUsers",
                column: "PatientInfoId",
                principalTable: "Patients",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffInfoId",
                table: "AppUsers",
                column: "StaffInfoId",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Patients_PatientInfoId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffInfoId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_PatientInfoId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "UserInfoId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "PatientInfoId",
                table: "AppUsers");

            migrationBuilder.RenameColumn(
                name: "StaffInfoId",
                table: "AppUsers",
                newName: "StaffId");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_StaffInfoId",
                table: "AppUsers",
                newName: "IX_AppUsers_StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_StaffMembers_StaffId",
                table: "AppUsers",
                column: "StaffId",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }
    }
}
