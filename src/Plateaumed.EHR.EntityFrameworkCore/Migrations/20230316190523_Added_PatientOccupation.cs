﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatientOccupation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientOccupations",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        TenantId = table.Column<int>(type: "integer", nullable: true),
                        Name = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: false
                        ),
                        PatientOccupationCategoryId = table.Column<long>(
                            type: "bigint",
                            nullable: false
                        ),
                        CreationTime = table.Column<DateTime>(
                            type: "timestamp without time zone",
                            nullable: false
                        ),
                        CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                        LastModificationTime = table.Column<DateTime>(
                            type: "timestamp without time zone",
                            nullable: true
                        ),
                        LastModifierUserId = table.Column<long>(type: "bigint", nullable: true)
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientOccupations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientOccupations_PatientOccupationCategories_PatientOccup~",
                        column: x => x.PatientOccupationCategoryId,
                        principalTable: "PatientOccupationCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_PatientOccupations_PatientOccupationCategoryId",
                table: "PatientOccupations",
                column: "PatientOccupationCategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PatientOccupations_TenantId",
                table: "PatientOccupations",
                column: "TenantId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PatientOccupations");
        }
    }
}
