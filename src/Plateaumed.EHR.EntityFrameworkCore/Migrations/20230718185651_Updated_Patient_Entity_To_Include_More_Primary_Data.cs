using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Authorization.Users;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedPatientEntityToIncludeMorePrimaryData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CountryId",
                table: "Patients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "Patients",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateId",
                table: "Patients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StateOfOriginId",
                table: "Patients",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<TitleType>(
                name: "Title",
                table: "Patients",
                type: "title_type",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CountryId",
                table: "Patients",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_StateId",
                table: "Patients",
                column: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Countries_CountryId",
                table: "Patients",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Regions_StateId",
                table: "Patients",
                column: "StateId",
                principalTable: "Regions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Countries_CountryId",
                table: "Patients");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Regions_StateId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_CountryId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_StateId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "StateId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "StateOfOriginId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Patients");
        }
    }
}
