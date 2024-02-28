using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddFacilityToBedType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "FacilityId",
                table: "BedTypes",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BedTypes_FacilityId",
                table: "BedTypes",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_BedTypes_Facilities_FacilityId",
                table: "BedTypes",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BedTypes_Facilities_FacilityId",
                table: "BedTypes");

            migrationBuilder.DropIndex(
                name: "IX_BedTypes_FacilityId",
                table: "BedTypes");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "BedTypes");
        }
    }
}
