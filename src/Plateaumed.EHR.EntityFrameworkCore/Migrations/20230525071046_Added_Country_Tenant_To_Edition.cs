using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountryTenantToEdition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "AppEditions",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "AppEditions",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppEditions_CountryId",
                table: "AppEditions",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_AppEditions_TenantId",
                table: "AppEditions",
                column: "TenantId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppEditions_Countries_CountryId",
                table: "AppEditions",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppEditions_Countries_CountryId",
                table: "AppEditions");

            migrationBuilder.DropIndex(
                name: "IX_AppEditions_CountryId",
                table: "AppEditions");

            migrationBuilder.DropIndex(
                name: "IX_AppEditions_TenantId",
                table: "AppEditions");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AppEditions");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "AppEditions");
        }
    }
}
