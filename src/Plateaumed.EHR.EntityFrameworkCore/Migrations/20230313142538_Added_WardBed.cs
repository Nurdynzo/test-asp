using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedWardBed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WardBeds",
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
                        Count = table.Column<int>(type: "integer", nullable: false),
                        IsActive = table.Column<bool>(type: "boolean", nullable: false),
                        BedTypeId = table.Column<long>(type: "bigint", nullable: false),
                        WardId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_WardBeds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WardBeds_BedTypes_BedTypeId",
                        column: x => x.BedTypeId,
                        principalTable: "BedTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_WardBeds_Wards_WardId",
                        column: x => x.WardId,
                        principalTable: "Wards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_WardBeds_BedTypeId",
                table: "WardBeds",
                column: "BedTypeId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_WardBeds_TenantId",
                table: "WardBeds",
                column: "TenantId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_WardBeds_WardId",
                table: "WardBeds",
                column: "WardId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "WardBeds");
        }
    }
}
