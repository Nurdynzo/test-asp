using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class MigrationChangeCityIdToCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientTravelHistories_Regions_CityId",
                table: "PatientTravelHistories");

            migrationBuilder.DropIndex(
                name: "IX_PatientTravelHistories_CityId",
                table: "PatientTravelHistories");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "PatientTravelHistories");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "PatientTravelHistories",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "PatientTravelHistories");

            migrationBuilder.AddColumn<int>(
                name: "CityId",
                table: "PatientTravelHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PatientTravelHistories_CityId",
                table: "PatientTravelHistories",
                column: "CityId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientTravelHistories_Regions_CityId",
                table: "PatientTravelHistories",
                column: "CityId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
