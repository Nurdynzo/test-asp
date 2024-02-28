using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Authorization.Users;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateUserEntityWithCommonProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .Annotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .Annotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .Annotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host");

            migrationBuilder.AddColumn<string>(
                name: "AltEmailAddress",
                table: "AppUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AppUsers",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<GenderType>(
                name: "Gender",
                table: "AppUsers",
                type: "gender_type",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentificationCode",
                table: "AppUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<IdentificationType>(
                name: "IdentificationType",
                table: "AppUsers",
                type: "identification_type",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsOnboarded",
                table: "AppUsers",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "AppUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<TitleType>(
                name: "Title",
                table: "AppUsers",
                type: "title_type",
                nullable: true);

            migrationBuilder.AddColumn<UserType>(
                name: "Type",
                table: "AppUsers",
                type: "user_type",
                nullable: false,
                defaultValue: UserType.TenantStaff);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AltEmailAddress",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "IdentificationCode",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "IdentificationType",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "IsOnboarded",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "AppUsers");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "AppUsers");

            migrationBuilder.AlterDatabase()
                .OldAnnotation("Npgsql:Enum:gender_type", "Male,Female,Other")
                .OldAnnotation("Npgsql:Enum:identification_type", "State_Id_Card,State_Driver_License,Military_Id_Card,Social_Security_Card,Birth_Certificate,Voter_Registration_Card")
                .OldAnnotation("Npgsql:Enum:title_type", "Mr,Mrs,Miss,Ms,Dr,Prof,Hon,Rev,Pr,Fr,Other")
                .OldAnnotation("Npgsql:Enum:user_type", "Tenant_Staff,Tenant_Patient,Host");
        }
    }
}
