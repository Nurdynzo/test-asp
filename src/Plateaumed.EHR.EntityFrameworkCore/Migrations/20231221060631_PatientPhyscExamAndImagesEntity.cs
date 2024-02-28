using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class PatientPhyscExamAndImagesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.CreateTable(
                name: "PatientPhysicalExaminations",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PhysicalExaminationEntryType = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PhysicalExaminationTypeId = table.Column<long>(type: "bigint", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    OtherNote = table.Column<string>(type: "varchar(2000)", nullable: true),
                    JsonData = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_PatientPhysicalExaminations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExaminations_AppUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExaminations_AppUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExaminations_AppUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExaminations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExaminations_PhysicalExaminationTypes_Physic~",
                        column: x => x.PhysicalExaminationTypeId,
                        principalTable: "PhysicalExaminationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientPhysicalExaminationImageFiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientPhysicalExaminationId = table.Column<long>(type: "bigint", nullable: false),
                    FileId = table.Column<string>(type: "varchar(300)", nullable: true),
                    FileName = table.Column<string>(type: "varchar(300)", nullable: true),
                    FileUrl = table.Column<string>(type: "varchar(2000)", nullable: true),
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
                    table.PrimaryKey("PK_PatientPhysicalExaminationImageFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientPhysicalExaminationImageFiles_PatientPhysicalExamina~",
                        column: x => x.PatientPhysicalExaminationId,
                        principalTable: "PatientPhysicalExaminations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExaminationImageFiles_PatientPhysicalExamina~",
                table: "PatientPhysicalExaminationImageFiles",
                column: "PatientPhysicalExaminationId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExaminations_CreatorUserId",
                table: "PatientPhysicalExaminations",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExaminations_DeleterUserId",
                table: "PatientPhysicalExaminations",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExaminations_LastModifierUserId",
                table: "PatientPhysicalExaminations",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExaminations_PatientId",
                table: "PatientPhysicalExaminations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExaminations_PhysicalExaminationTypeId",
                table: "PatientPhysicalExaminations",
                column: "PhysicalExaminationTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientPhysicalExaminations_TenantId_PatientId",
                table: "PatientPhysicalExaminations",
                columns: new[] { "TenantId", "PatientId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientPhysicalExaminationImageFiles");

            migrationBuilder.DropTable(
                name: "PatientPhysicalExaminations"); 
        }
    }
}
