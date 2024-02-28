using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Authorization.Users;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatientRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientRelations",
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
                        Relationship = table.Column<int>(type: "integer", nullable: false),
                        FirstName = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: false
                        ),
                        MiddleName = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        LastName = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: false
                        ),
                        Title = table.Column<TitleType>(type: "title_type", nullable: false),
                        Address = table.Column<string>(
                            type: "character varying(512)",
                            maxLength: 512,
                            nullable: true
                        ),
                        PhoneNumber = table.Column<string>(
                            type: "character varying(24)",
                            maxLength: 24,
                            nullable: true
                        ),
                        Email = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        IsGuardian = table.Column<bool>(type: "boolean", nullable: false),
                        PatientId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_PatientRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientRelations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_PatientRelations_PatientId",
                table: "PatientRelations",
                column: "PatientId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PatientRelations_TenantId",
                table: "PatientRelations",
                column: "TenantId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PatientRelations");
        }
    }
}
