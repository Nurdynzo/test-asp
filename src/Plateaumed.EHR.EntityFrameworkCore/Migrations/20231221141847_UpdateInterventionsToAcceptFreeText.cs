using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInterventionsToAcceptFreeText : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientInterventions_NursingInterventions_InterventionId",
                table: "PatientInterventions");

            migrationBuilder.DropIndex(
                name: "IX_PatientInterventions_InterventionId",
                table: "PatientInterventions");

            migrationBuilder.DropColumn(
                name: "InterventionId",
                table: "PatientInterventions");

            migrationBuilder.AddColumn<string>(
                name: "EventText",
                table: "PatientInterventions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InterventionEvents",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InterventionText = table.Column<string>(type: "text", nullable: true),
                    NursingInterventionId = table.Column<long>(type: "bigint", nullable: true),
                    PatientInterventionId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_InterventionEvents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InterventionEvents_NursingInterventions_NursingIntervention~",
                        column: x => x.NursingInterventionId,
                        principalTable: "NursingInterventions",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_InterventionEvents_PatientInterventions_PatientIntervention~",
                        column: x => x.PatientInterventionId,
                        principalTable: "PatientInterventions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InterventionEvents_NursingInterventionId",
                table: "InterventionEvents",
                column: "NursingInterventionId");

            migrationBuilder.CreateIndex(
                name: "IX_InterventionEvents_PatientInterventionId",
                table: "InterventionEvents",
                column: "PatientInterventionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InterventionEvents");

            migrationBuilder.DropColumn(
                name: "EventText",
                table: "PatientInterventions");

            migrationBuilder.AddColumn<long>(
                name: "InterventionId",
                table: "PatientInterventions",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_PatientInterventions_InterventionId",
                table: "PatientInterventions",
                column: "InterventionId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientInterventions_NursingInterventions_InterventionId",
                table: "PatientInterventions",
                column: "InterventionId",
                principalTable: "NursingInterventions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
