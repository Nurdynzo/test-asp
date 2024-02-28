using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatientVitalAndGCSScoringEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {  
            migrationBuilder.AddColumn<int>(
                name: "DecimalPlaces",
                table: "VitalSigns",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "VitalReading",
                table: "PatientVitals",
                type: "varchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PatientVitalType",
                table: "PatientVitals",
                type: "integer",
                nullable: false,
                defaultValue: 0); 

            migrationBuilder.CreateTable(
                name: "PatientGcsScoring",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    ProcedureId = table.Column<long>(type: "bigint", nullable: true),
                    EyeOpening = table.Column<int>(type: "integer", nullable: false),
                    VerbalResponse = table.Column<int>(type: "integer", nullable: false),
                    MotorResponse = table.Column<int>(type: "integer", nullable: false),
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
                name: "IX_PatientVitals_CreatorUserId",
                table: "PatientVitals",
                column: "CreatorUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_DeleterUserId",
                table: "PatientVitals",
                column: "DeleterUserId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientVitals_LastModifierUserId",
                table: "PatientVitals",
                column: "LastModifierUserId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PatientVitals_AppUsers_CreatorUserId",
                table: "PatientVitals",
                column: "CreatorUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientVitals_AppUsers_DeleterUserId",
                table: "PatientVitals",
                column: "DeleterUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientVitals_AppUsers_LastModifierUserId",
                table: "PatientVitals",
                column: "LastModifierUserId",
                principalTable: "AppUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientVitals_AppUsers_CreatorUserId",
                table: "PatientVitals");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientVitals_AppUsers_DeleterUserId",
                table: "PatientVitals");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientVitals_AppUsers_LastModifierUserId",
                table: "PatientVitals"); 

            migrationBuilder.DropTable(
                name: "PatientGcsScoring");

            migrationBuilder.DropIndex(
                name: "IX_PatientVitals_CreatorUserId",
                table: "PatientVitals");

            migrationBuilder.DropIndex(
                name: "IX_PatientVitals_DeleterUserId",
                table: "PatientVitals");

            migrationBuilder.DropIndex(
                name: "IX_PatientVitals_LastModifierUserId",
                table: "PatientVitals");

            migrationBuilder.DropColumn(
                name: "DecimalPlaces",
                table: "VitalSigns");

            migrationBuilder.DropColumn(
                name: "PatientVitalType",
                table: "PatientVitals");  

            migrationBuilder.AlterColumn<string>(
                name: "VitalReading",
                table: "PatientVitals",
                type: "varchar(100)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(100)"); 
        }
    }
}
