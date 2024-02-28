using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RemoveGCSScoringEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientGcsScoring"); 
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        { 
            migrationBuilder.CreateTable(
                name: "PatientGcsScoring",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    EyeOpening = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    MotorResponse = table.Column<int>(type: "integer", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    VerbalResponse = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientGcsScoring", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientGcsScoring_AppUsers_CreatorUserId",
                        column: x => x.CreatorUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientGcsScoring_AppUsers_DeleterUserId",
                        column: x => x.DeleterUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientGcsScoring_AppUsers_LastModifierUserId",
                        column: x => x.LastModifierUserId,
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientGcsScoring_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientGcsScoring_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "Procedures",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientGcsScoring_CreatorUserId",
                table: "PatientGcsScoring",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientGcsScoring_DeleterUserId",
                table: "PatientGcsScoring",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientGcsScoring_LastModifierUserId",
                table: "PatientGcsScoring",
                column: "LastModifierUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientGcsScoring_PatientId",
                table: "PatientGcsScoring",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientGcsScoring_ProcedureId",
                table: "PatientGcsScoring",
                column: "ProcedureId");
        }
    }
}
