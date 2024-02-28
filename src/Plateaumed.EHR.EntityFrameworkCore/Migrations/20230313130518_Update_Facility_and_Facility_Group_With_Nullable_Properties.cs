using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFacilityandFacilityGroupWithNullableProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_PatientCodeTemplates_PatientCodeTemplateId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_StaffCodeTemplates_StaffCodeTemplateId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_FacilityGroups_PatientCodeTemplates_PatientCodeTemplateId",
                table: "FacilityGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_FacilityGroups_StaffCodeTemplates_StaffCodeTemplateId",
                table: "FacilityGroups");

            migrationBuilder.AlterColumn<long>(
                name: "StaffCodeTemplateId",
                table: "FacilityGroups",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PatientCodeTemplateId",
                table: "FacilityGroups",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "FacilityGroups",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "FacilityGroups",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "FacilityGroups",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionTime",
                table: "FacilityGroups",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "FacilityGroups",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModificationTime",
                table: "FacilityGroups",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "FacilityGroups",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StaffCodeTemplateId",
                table: "Facilities",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<long>(
                name: "PatientCodeTemplateId",
                table: "Facilities",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Facilities",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_PatientCodeTemplates_PatientCodeTemplateId",
                table: "Facilities",
                column: "PatientCodeTemplateId",
                principalTable: "PatientCodeTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_StaffCodeTemplates_StaffCodeTemplateId",
                table: "Facilities",
                column: "StaffCodeTemplateId",
                principalTable: "StaffCodeTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityGroups_PatientCodeTemplates_PatientCodeTemplateId",
                table: "FacilityGroups",
                column: "PatientCodeTemplateId",
                principalTable: "PatientCodeTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityGroups_StaffCodeTemplates_StaffCodeTemplateId",
                table: "FacilityGroups",
                column: "StaffCodeTemplateId",
                principalTable: "StaffCodeTemplates",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_PatientCodeTemplates_PatientCodeTemplateId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_StaffCodeTemplates_StaffCodeTemplateId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_FacilityGroups_PatientCodeTemplates_PatientCodeTemplateId",
                table: "FacilityGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_FacilityGroups_StaffCodeTemplates_StaffCodeTemplateId",
                table: "FacilityGroups");

            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "FacilityGroups");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "FacilityGroups");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "FacilityGroups");

            migrationBuilder.DropColumn(
                name: "DeletionTime",
                table: "FacilityGroups");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "FacilityGroups");

            migrationBuilder.DropColumn(
                name: "LastModificationTime",
                table: "FacilityGroups");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "FacilityGroups");

            migrationBuilder.AlterColumn<long>(
                name: "StaffCodeTemplateId",
                table: "FacilityGroups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PatientCodeTemplateId",
                table: "FacilityGroups",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "StaffCodeTemplateId",
                table: "Facilities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "PatientCodeTemplateId",
                table: "Facilities",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Level",
                table: "Facilities",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_PatientCodeTemplates_PatientCodeTemplateId",
                table: "Facilities",
                column: "PatientCodeTemplateId",
                principalTable: "PatientCodeTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_StaffCodeTemplates_StaffCodeTemplateId",
                table: "Facilities",
                column: "StaffCodeTemplateId",
                principalTable: "StaffCodeTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityGroups_PatientCodeTemplates_PatientCodeTemplateId",
                table: "FacilityGroups",
                column: "PatientCodeTemplateId",
                principalTable: "PatientCodeTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FacilityGroups_StaffCodeTemplates_StaffCodeTemplateId",
                table: "FacilityGroups",
                column: "StaffCodeTemplateId",
                principalTable: "StaffCodeTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
