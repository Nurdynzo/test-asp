using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddDischargeNoteAndCauseOfDeath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CausesOfDeath",
                table: "Discharges");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Discharges");

            migrationBuilder.RenameColumn(
                name: "isBroughtInDead",
                table: "Discharges",
                newName: "IsBroughtInDead");

            migrationBuilder.AddColumn<string>(
                name: "ActivitiesOfDailyLiving",
                table: "Discharges",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Drugs",
                table: "Discharges",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientAssessment",
                table: "Discharges",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "DischargeNotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    DischargeId = table.Column<long>(type: "bigint", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_DischargeNotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DischargeNotes_Discharges_DischargeId",
                        column: x => x.DischargeId,
                        principalTable: "Discharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientCausesOfDeath",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    DischargeId = table.Column<long>(type: "bigint", nullable: false),
                    CausesOfDeath = table.Column<string>(type: "text", nullable: true),
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
                    table.PrimaryKey("PK_PatientCausesOfDeath", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientCausesOfDeath_Discharges_DischargeId",
                        column: x => x.DischargeId,
                        principalTable: "Discharges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DischargeNotes_DischargeId",
                table: "DischargeNotes",
                column: "DischargeId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCausesOfDeath_DischargeId",
                table: "PatientCausesOfDeath",
                column: "DischargeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DischargeNotes");

            migrationBuilder.DropTable(
                name: "PatientCausesOfDeath");

            migrationBuilder.DropColumn(
                name: "ActivitiesOfDailyLiving",
                table: "Discharges");

            migrationBuilder.DropColumn(
                name: "Drugs",
                table: "Discharges");

            migrationBuilder.DropColumn(
                name: "PatientAssessment",
                table: "Discharges");

            migrationBuilder.RenameColumn(
                name: "IsBroughtInDead",
                table: "Discharges",
                newName: "isBroughtInDead");

            migrationBuilder.AddColumn<string>(
                name: "CausesOfDeath",
                table: "Discharges",
                type: "varchar(2000)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Discharges",
                type: "varchar(2000)",
                nullable: true);
        }
    }
}
