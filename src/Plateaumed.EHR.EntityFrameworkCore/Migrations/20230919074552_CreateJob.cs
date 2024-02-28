using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class CreateJob : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_JobLevels_JobLevelId",
                table: "StaffMembers");

            migrationBuilder.DropForeignKey(
                name: "FK_StaffMembers_JobTitles_JobTitleId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_StaffMembers_JobLevelId",
                table: "StaffMembers");

            migrationBuilder.DropIndex(
                name: "IX_StaffMembers_JobTitleId",
                table: "StaffMembers");

            migrationBuilder.DropColumn(
                name: "JobLevelId",
                table: "StaffMembers");

            migrationBuilder.DropColumn(
                name: "JobTitleId",
                table: "StaffMembers");

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    IsPrimary = table.Column<bool>(type: "boolean", nullable: false),
                    StaffMemberId = table.Column<long>(type: "bigint", nullable: false),
                    JobLevelId = table.Column<long>(type: "bigint", nullable: true),
                    FacilityId = table.Column<long>(type: "bigint", nullable: true),
                    UnitId = table.Column<long>(type: "bigint", nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jobs_AppOrganizationUnitsExtended_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "AppOrganizationUnitsExtended",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_AppOrganizationUnitsExtended_UnitId",
                        column: x => x.UnitId,
                        principalTable: "AppOrganizationUnitsExtended",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_JobLevels_JobLevelId",
                        column: x => x.JobLevelId,
                        principalTable: "JobLevels",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Jobs_StaffMembers_StaffMemberId",
                        column: x => x.StaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobServiceCentre",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
                    ServiceCentre = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_JobServiceCentre", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobServiceCentre_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WardJob",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    WardId = table.Column<long>(type: "bigint", nullable: false),
                    JobId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_WardJob", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WardJob_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WardJob_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_DepartmentId",
                table: "Jobs",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_FacilityId",
                table: "Jobs",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobLevelId",
                table: "Jobs",
                column: "JobLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_StaffMemberId",
                table: "Jobs",
                column: "StaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_TenantId",
                table: "Jobs",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UnitId",
                table: "Jobs",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_JobServiceCentre_JobId",
                table: "JobServiceCentre",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_JobServiceCentre_TenantId",
                table: "JobServiceCentre",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WardJob_JobId",
                table: "WardJob",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_WardJob_TenantId",
                table: "WardJob",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_WardJob_WardId",
                table: "WardJob",
                column: "WardId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobServiceCentre");

            migrationBuilder.DropTable(
                name: "WardJob");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.AddColumn<long>(
                name: "JobLevelId",
                table: "StaffMembers",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "JobTitleId",
                table: "StaffMembers",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_JobLevelId",
                table: "StaffMembers",
                column: "JobLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_StaffMembers_JobTitleId",
                table: "StaffMembers",
                column: "JobTitleId");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_JobLevels_JobLevelId",
                table: "StaffMembers",
                column: "JobLevelId",
                principalTable: "JobLevels",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StaffMembers_JobTitles_JobTitleId",
                table: "StaffMembers",
                column: "JobTitleId",
                principalTable: "JobTitles",
                principalColumn: "Id");
        }
    }
}
