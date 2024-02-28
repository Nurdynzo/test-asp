using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddFullAuditToPatientPastMedicalCondition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "PatientPastMedicalConditions",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "PatientPastMedicalConditions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "PatientPastMedicalConditions",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "PatientPastMedicalConditions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PatientPastMedicalConditions",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "PatientPastMedicalConditions",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "PatientPastMedicalConditions",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "PatientPastMedicalConditions");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "PatientPastMedicalConditions");
        }
    }
}
