using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFacilityGroupStaffCodeTemplateandPatientCodeTemplateEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_StaffCodeTemplates_TenantId",
                table: "StaffCodeTemplates");

            migrationBuilder.DropIndex(
                name: "IX_PatientCodeTemplates_TenantId",
                table: "PatientCodeTemplates");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "StaffCodeTemplates");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "PatientCodeTemplates");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "StaffCodeTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "PatientCodeTemplates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StaffCodeTemplates_TenantId",
                table: "StaffCodeTemplates",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCodeTemplates_TenantId",
                table: "PatientCodeTemplates",
                column: "TenantId");
        }
    }
}
