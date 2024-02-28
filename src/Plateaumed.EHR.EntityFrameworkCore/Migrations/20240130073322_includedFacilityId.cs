using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class includedFacilityId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationResultReviewer_StaffMembers_StaffMemberId",
                table: "InvestigationResultReviewer");

            migrationBuilder.AlterColumn<long>(
                name: "StaffMemberId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "FacilityId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationResultReviewer_StaffMembers_StaffMemberId",
                table: "InvestigationResultReviewer",
                column: "StaffMemberId",
                principalTable: "StaffMembers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InvestigationResultReviewer_StaffMembers_StaffMemberId",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "InvestigationResultReviewer");

            migrationBuilder.AlterColumn<long>(
                name: "StaffMemberId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_InvestigationResultReviewer_StaffMembers_StaffMemberId",
                table: "InvestigationResultReviewer",
                column: "StaffMemberId",
                principalTable: "StaffMembers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
