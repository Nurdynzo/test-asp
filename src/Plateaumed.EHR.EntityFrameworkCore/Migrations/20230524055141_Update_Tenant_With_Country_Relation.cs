using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTenantWithCountryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "AppTenants",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppTenants_CountryId",
                table: "AppTenants",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppTenants_Countries_CountryId",
                table: "AppTenants",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppTenants_Countries_CountryId",
                table: "AppTenants");

            migrationBuilder.DropIndex(
                name: "IX_AppTenants_CountryId",
                table: "AppTenants");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AppTenants");
        }
    }
}
