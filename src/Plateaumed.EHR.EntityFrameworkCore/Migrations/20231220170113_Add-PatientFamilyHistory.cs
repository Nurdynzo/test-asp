using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Patients;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientFamilyHistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientFamilyHistories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PatientId = table.Column<long>(type: "bigint", nullable: false),
                    IsFamilyHistoryKnown = table.Column<bool>(type: "boolean", nullable: false),
                    TotalNumberOfSiblings = table.Column<int>(type: "integer", nullable: false),
                    TotalNumberOfMaleSiblings = table.Column<int>(type: "integer", nullable: false),
                    TotalNumberOfFemaleSiblings = table.Column<int>(type: "integer", nullable: false),
                    TotalNumberOfChildren = table.Column<int>(type: "integer", nullable: false),
                    TotalNumberOfMaleChildren = table.Column<int>(type: "integer", nullable: false),
                    TotalNumberOfFemaleChildren = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_PatientFamilyHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientFamilyHistories_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientFamilyMembers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Relationship = table.Column<Relationship>(type: "relationship", nullable: false),
                    IsAlive = table.Column<bool>(type: "boolean", nullable: false),
                    AgeAtDeath = table.Column<int>(type: "integer", nullable: false),
                    CauseOfDeath = table.Column<List<string>>(type: "text[]", nullable: true),
                    AgeAtDiagnosis = table.Column<int>(type: "integer", nullable: false),
                    PatientFamilyHistoryId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_PatientFamilyMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientFamilyMembers_PatientFamilyHistories_PatientFamilyHi~",
                        column: x => x.PatientFamilyHistoryId,
                        principalTable: "PatientFamilyHistories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientFamilyHistories_PatientId",
                table: "PatientFamilyHistories",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientFamilyMembers_PatientFamilyHistoryId",
                table: "PatientFamilyMembers",
                column: "PatientFamilyHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientFamilyMembers");

            migrationBuilder.DropTable(
                name: "PatientFamilyHistories");
        }
    }
}
