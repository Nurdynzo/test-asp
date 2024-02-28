﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatientCodeTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientCodeTemplates",
                columns: table =>
                    new
                    {
                        Id = table
                            .Column<long>(type: "bigint", nullable: false)
                            .Annotation(
                                "Npgsql:ValueGenerationStrategy",
                                NpgsqlValueGenerationStrategy.IdentityByDefaultColumn
                            ),
                        TenantId = table.Column<int>(type: "integer", nullable: false),
                        Prefix = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        Length = table.Column<int>(type: "integer", nullable: false),
                        Suffix = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        StartingIndex = table.Column<int>(type: "integer", nullable: false),
                        CreationTime = table.Column<DateTime>(
                            type: "timestamp without time zone",
                            nullable: false
                        ),
                        CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                        LastModificationTime = table.Column<DateTime>(
                            type: "timestamp without time zone",
                            nullable: true
                        ),
                        LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                        IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                        DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                        DeletionTime = table.Column<DateTime>(
                            type: "timestamp without time zone",
                            nullable: true
                        )
                    },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCodeTemplates", x => x.Id);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_PatientCodeTemplates_TenantId",
                table: "PatientCodeTemplates",
                column: "TenantId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PatientCodeTemplates");
        }
    }
}
