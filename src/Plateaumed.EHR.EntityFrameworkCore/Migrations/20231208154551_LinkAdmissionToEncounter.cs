using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class LinkAdmissionToEncounter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AdmissionId",
                table: "PatientEncounters",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "FacilityId",
                table: "PatientEncounters",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceCentre",
                table: "PatientEncounters",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "UnitId",
                table: "PatientEncounters",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WardBedId",
                table: "PatientEncounters",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "WardId",
                table: "PatientEncounters",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admissions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    AttendingPhysicianId = table.Column<long>(type: "bigint", nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    FacilityId = table.Column<long>(type: "bigint", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Admissions_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Admissions_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Admissions_StaffMembers_AttendingPhysicianId",
                        column: x => x.AttendingPhysicianId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "StaffEncounters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    StaffId = table.Column<long>(type: "bigint", nullable: false),
                    EncounterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffEncounters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StaffEncounters_PatientEncounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "PatientEncounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffEncounters_StaffMembers_StaffId",
                        column: x => x.StaffId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientEncounters_AdmissionId",
                table: "PatientEncounters",
                column: "AdmissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientEncounters_FacilityId",
                table: "PatientEncounters",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientEncounters_UnitId",
                table: "PatientEncounters",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientEncounters_WardBedId",
                table: "PatientEncounters",
                column: "WardBedId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientEncounters_WardId",
                table: "PatientEncounters",
                column: "WardId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_AttendingPhysicianId",
                table: "Admissions",
                column: "AttendingPhysicianId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_FacilityId",
                table: "Admissions",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Admissions_PatientId",
                table: "Admissions",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffEncounters_EncounterId",
                table: "StaffEncounters",
                column: "EncounterId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffEncounters_StaffId",
                table: "StaffEncounters",
                column: "StaffId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientEncounters_Admissions_AdmissionId",
                table: "PatientEncounters",
                column: "AdmissionId",
                principalTable: "Admissions",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientEncounters_AppOrganizationUnitsExtended_UnitId",
                table: "PatientEncounters",
                column: "UnitId",
                principalTable: "AppOrganizationUnitsExtended",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientEncounters_Facilities_FacilityId",
                table: "PatientEncounters",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientEncounters_WardBeds_WardBedId",
                table: "PatientEncounters",
                column: "WardBedId",
                principalTable: "WardBeds",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientEncounters_Wards_WardId",
                table: "PatientEncounters",
                column: "WardId",
                principalTable: "Wards",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientEncounters_Admissions_AdmissionId",
                table: "PatientEncounters");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientEncounters_AppOrganizationUnitsExtended_UnitId",
                table: "PatientEncounters");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientEncounters_Facilities_FacilityId",
                table: "PatientEncounters");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientEncounters_WardBeds_WardBedId",
                table: "PatientEncounters");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientEncounters_Wards_WardId",
                table: "PatientEncounters");

            migrationBuilder.DropTable(
                name: "Admissions");

            migrationBuilder.DropTable(
                name: "StaffEncounters");

            migrationBuilder.DropIndex(
                name: "IX_PatientEncounters_AdmissionId",
                table: "PatientEncounters");

            migrationBuilder.DropIndex(
                name: "IX_PatientEncounters_FacilityId",
                table: "PatientEncounters");

            migrationBuilder.DropIndex(
                name: "IX_PatientEncounters_UnitId",
                table: "PatientEncounters");

            migrationBuilder.DropIndex(
                name: "IX_PatientEncounters_WardBedId",
                table: "PatientEncounters");

            migrationBuilder.DropIndex(
                name: "IX_PatientEncounters_WardId",
                table: "PatientEncounters");

            migrationBuilder.DropColumn(
                name: "AdmissionId",
                table: "PatientEncounters");

            migrationBuilder.DropColumn(
                name: "FacilityId",
                table: "PatientEncounters");

            migrationBuilder.DropColumn(
                name: "ServiceCentre",
                table: "PatientEncounters");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "PatientEncounters");

            migrationBuilder.DropColumn(
                name: "WardBedId",
                table: "PatientEncounters");

            migrationBuilder.DropColumn(
                name: "WardId",
                table: "PatientEncounters");
        }
    }
}
