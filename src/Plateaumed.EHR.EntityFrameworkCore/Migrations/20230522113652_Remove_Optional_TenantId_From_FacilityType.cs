using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOptionalTenantIdFromFacilityType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FacilityTypes_TenantId",
                table: "FacilityTypes");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "FacilityTypes");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityTypes_Name",
                table: "FacilityTypes",
                column: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FacilityTypes_Name",
                table: "FacilityTypes");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "FacilityTypes",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FacilityTypes_TenantId",
                table: "FacilityTypes",
                column: "TenantId");
        }
    }
}
