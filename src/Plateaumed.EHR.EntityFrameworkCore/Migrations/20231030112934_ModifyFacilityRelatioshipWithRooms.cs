using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifyFacilityRelatioshipWithRooms : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FacilityId",
                table: "Rooms",
                column: "FacilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Facilities_FacilityId",
                table: "Rooms",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Facilities_FacilityId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_FacilityId",
                table: "Rooms");
        }
    }
}
