using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddNurseSpecializedProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "SpecializedProcedureNurseDetails",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    TimePatientReceived = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    ScrubStaffMemberId = table.Column<long>(type: "bigint", nullable: true),
                    CirculatingStaffMemberId = table.Column<long>(type: "bigint", nullable: true),
                    SpecializedProcedureProcedureId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_SpecializedProcedureNurseDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpecializedProcedureNurseDetails_SpecializedProcedures_Spec~",
                        column: x => x.SpecializedProcedureProcedureId,
                        principalTable: "SpecializedProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SpecializedProcedureNurseDetails_StaffMembers_CirculatingSt~",
                        column: x => x.CirculatingStaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SpecializedProcedureNurseDetails_StaffMembers_ScrubStaffMem~",
                        column: x => x.ScrubStaffMemberId,
                        principalTable: "StaffMembers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SpecializedProcedureNurseDetails_CirculatingStaffMemberId",
                table: "SpecializedProcedureNurseDetails",
                column: "CirculatingStaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecializedProcedureNurseDetails_ScrubStaffMemberId",
                table: "SpecializedProcedureNurseDetails",
                column: "ScrubStaffMemberId");

            migrationBuilder.CreateIndex(
                name: "IX_SpecializedProcedureNurseDetails_SpecializedProcedureProced~",
                table: "SpecializedProcedureNurseDetails",
                column: "SpecializedProcedureProcedureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SpecializedProcedureNurseDetails");
        }
    }
}
