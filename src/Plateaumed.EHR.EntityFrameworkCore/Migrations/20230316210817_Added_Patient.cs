using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
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
                        PatientCode = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: false
                        ),
                        Address = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        District = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        Ethnicity = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        Religion = table.Column<int>(type: "integer", nullable: false),
                        MaritalStatus = table.Column<int>(type: "integer", nullable: false),
                        BloodGroup = table.Column<int>(type: "integer", nullable: false),
                        BloodGenotype = table.Column<int>(type: "integer", nullable: false),
                        LocationOfWork = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        NuclearFamilySize = table.Column<int>(type: "integer", nullable: false),
                        NumberOfSiblings = table.Column<int>(type: "integer", nullable: false),
                        PositionInFamily = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        NumberOfChildren = table.Column<int>(type: "integer", nullable: false),
                        CountryId = table.Column<long>(type: "bigint", nullable: true),
                        PatientOccupationId = table.Column<long>(type: "bigint", nullable: true),
                        PatientOccupationCategoryId = table.Column<long>(
                            type: "bigint",
                            nullable: true
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
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Patients_PatientOccupationCategories_PatientOccupationCateg~",
                        column: x => x.PatientOccupationCategoryId,
                        principalTable: "PatientOccupationCategories",
                        principalColumn: "Id"
                    );
                    table.ForeignKey(
                        name: "FK_Patients_PatientOccupations_PatientOccupationId",
                        column: x => x.PatientOccupationId,
                        principalTable: "PatientOccupations",
                        principalColumn: "Id"
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CountryId",
                table: "Patients",
                column: "CountryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientOccupationCategoryId",
                table: "Patients",
                column: "PatientOccupationCategoryId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Patients_PatientOccupationId",
                table: "Patients",
                column: "PatientOccupationId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_Patients_TenantId",
                table: "Patients",
                column: "TenantId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Patients");
        }
    }
}
