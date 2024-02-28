using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class LinkedCountryToInsuranceProvider : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "InsuranceProviders",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_InsuranceProviders_CountryId",
                table: "InsuranceProviders",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_InsuranceProviders_Countries_CountryId",
                table: "InsuranceProviders",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InsuranceProviders_Countries_CountryId",
                table: "InsuranceProviders");

            migrationBuilder.DropIndex(
                name: "IX_InsuranceProviders_CountryId",
                table: "InsuranceProviders");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "InsuranceProviders");
        }
    }
}
