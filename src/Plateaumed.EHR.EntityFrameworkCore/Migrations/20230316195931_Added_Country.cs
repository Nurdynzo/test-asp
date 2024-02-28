using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedCountry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
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
                        Nationality = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: false
                        ),
                        Code = table.Column<string>(
                            type: "character varying(3)",
                            maxLength: 3,
                            nullable: false
                        ),
                        PhoneCode = table.Column<string>(
                            type: "character varying(8)",
                            maxLength: 8,
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
                    table.PrimaryKey("PK_Countries", x => x.Id);
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Countries_TenantId",
                table: "Countries",
                column: "TenantId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Countries");
        }
    }
}
