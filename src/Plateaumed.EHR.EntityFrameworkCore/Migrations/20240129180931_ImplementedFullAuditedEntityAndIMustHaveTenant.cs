using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ImplementedFullAuditedEntityAndIMustHaveTenant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "InvestigationResultReviewer",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "InvestigationResultReviewer",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "InvestigationResultReviewer",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "InvestigationResultReviewer",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "InvestigationResultReviewer",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TenantId",
                table: "InvestigationResultReviewer",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "InvestigationResultReviewer");

            migrationBuilder.DropColumn(
                name: "TenantId",
                table: "InvestigationResultReviewer");
        }
    }
}
