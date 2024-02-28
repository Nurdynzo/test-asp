using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class DroppedUneccessaryColumnsInOccupationRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PatientOccupations_TenantId",
                table: "PatientOccupations");

            migrationBuilder.DropIndex(
                name: "IX_OccupationCategories_TenantId",
                table: "OccupationCategories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "PatientOccupations");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "PatientOccupations");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "Occupations");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "OccupationCategories");

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "PatientOccupations",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "PatientOccupations",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "PatientOccupations",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "PatientOccupations");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "PatientOccupations");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "PatientOccupations");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "PatientOccupations",
                type: "character varying(120)",
                maxLength: 120,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "PatientOccupations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "Occupations",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "OccupationCategories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientOccupations_TenantId",
                table: "PatientOccupations",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_OccupationCategories_TenantId",
                table: "OccupationCategories",
                column: "TenantId");
        }
    }
}
