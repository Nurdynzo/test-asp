using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedFacilityDocument : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacilityDocuments",
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
                        FileName = table.Column<string>(
                            type: "character varying(512)",
                            maxLength: 512,
                            nullable: true
                        ),
                        DocumentType = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: false
                        ),
                        Document = table.Column<Guid>(type: "uuid", nullable: true),
                        FacilityGroupId = table.Column<long>(type: "bigint", nullable: true),
                        FacilityId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_FacilityDocuments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityDocuments_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_FacilityDocuments_FacilityGroups_FacilityGroupId",
                        column: x => x.FacilityGroupId,
                        principalTable: "FacilityGroups",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_FacilityDocuments_FacilityGroupId",
                table: "FacilityDocuments",
                column: "FacilityGroupId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_FacilityDocuments_FacilityId",
                table: "FacilityDocuments",
                column: "FacilityId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_FacilityDocuments_TenantId",
                table: "FacilityDocuments",
                column: "TenantId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "FacilityDocuments");
        }
    }
}
