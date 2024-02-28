using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class FamilyHistoryCorrectionMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CauseOfDeath",
                table: "PatientFamilyMembers");

            migrationBuilder.AddColumn<List<string>>(
                name: "CausesOfDeath",
                table: "PatientFamilyMembers",
                type: "text[]",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "SeriousIllnesses",
                table: "PatientFamilyMembers",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CausesOfDeath",
                table: "PatientFamilyMembers");

            migrationBuilder.DropColumn(
                name: "SeriousIllnesses",
                table: "PatientFamilyMembers");

            migrationBuilder.AddColumn<string>(
                name: "CauseOfDeath",
                table: "PatientFamilyMembers",
                type: "text",
                nullable: true);
        }
    }
}
