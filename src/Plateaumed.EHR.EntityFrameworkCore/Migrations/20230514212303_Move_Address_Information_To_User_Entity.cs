using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Authorization.Users;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class MoveAddressInformationToUserEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Countries_CountryId",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_CountryId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Address",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "District",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppUsers");

            migrationBuilder.AlterColumn<string>(
                name: "IdentificationCode",
                table: "AppUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AppUsers",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "AppUsers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "AppUsers",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostCode",
                table: "AppUsers",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "State",
                table: "AppUsers",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StreetAddress",
                table: "AppUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_CountryId",
                table: "AppUsers",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_Countries_CountryId",
                table: "AppUsers",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_Countries_CountryId",
                table: "AppUsers");

            migrationBuilder.DropIndex(
                name: "IX_AppUsers_CountryId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "CountryId",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "District",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "PostCode",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "State",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "StreetAddress",
                table: "AppUsers");

            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Patients",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CountryId",
                table: "Patients",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "District",
                table: "Patients",
                type: "character varying(120)",
                maxLength: 120,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IdentificationCode",
                table: "AppUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<UserType>(
                name: "Type",
                table: "AppUsers",
                type: "user_type",
                nullable: false,
                defaultValue: UserType.TenantStaff);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CountryId",
                table: "Patients",
                column: "CountryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Countries_CountryId",
                table: "Patients",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id");
        }
    }
}
