using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedFacility : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Facilities",
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
                        Name = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: false
                        ),
                        IsActive = table.Column<bool>(type: "boolean", nullable: false),
                        EmailAddress = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        PhoneNumber = table.Column<string>(
                            type: "character varying(24)",
                            maxLength: 24,
                            nullable: true
                        ),
                        Website = table.Column<string>(
                            type: "character varying(512)",
                            maxLength: 512,
                            nullable: true
                        ),
                        Address = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        City = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        State = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        PostCode = table.Column<string>(
                            type: "character varying(10)",
                            maxLength: 10,
                            nullable: true
                        ),
                        BankAccountHolder = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        BankAccountNumber = table.Column<string>(
                            type: "character varying(256)",
                            maxLength: 256,
                            nullable: true
                        ),
                        UseGroupAddress = table.Column<bool>(type: "boolean", nullable: false),
                        UseGroupContacts = table.Column<bool>(type: "boolean", nullable: false),
                        UseGroupBilling = table.Column<bool>(type: "boolean", nullable: false),
                        GroupId = table.Column<long>(type: "bigint", nullable: false),
                        TypeId = table.Column<long>(type: "bigint", nullable: false),
                        PatientCodeTemplateId = table.Column<long>(type: "bigint", nullable: false),
                        StaffCodeTemplateId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_Facilities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityGroups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "FacilityGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Facilities_FacilityTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "FacilityTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Facilities_PatientCodeTemplates_PatientCodeTemplateId",
                        column: x => x.PatientCodeTemplateId,
                        principalTable: "PatientCodeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_Facilities_StaffCodeTemplates_StaffCodeTemplateId",
                        column: x => x.StaffCodeTemplateId,
                        principalTable: "StaffCodeTemplates",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_GroupId",
                table: "Facilities",
                column: "GroupId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_PatientCodeTemplateId",
                table: "Facilities",
                column: "PatientCodeTemplateId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_StaffCodeTemplateId",
                table: "Facilities",
                column: "StaffCodeTemplateId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_TenantId",
                table: "Facilities",
                column: "TenantId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_TypeId",
                table: "Facilities",
                column: "TypeId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Facilities");
        }
    }
}
