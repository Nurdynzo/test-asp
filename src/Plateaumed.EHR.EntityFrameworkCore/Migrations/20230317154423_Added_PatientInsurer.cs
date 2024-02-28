using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Insurance;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedPatientInsurer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PatientInsurers",
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
                        Type = table.Column<InsuranceProviderType>(
                            type: "insurance_provider_type",
                            nullable: false
                        ),
                        BenefiaryType = table.Column<int>(type: "integer", nullable: false),
                        Coverage = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        StartDate = table.Column<DateTime>(
                            type: "timestamp without time zone",
                            nullable: false
                        ),
                        EndDate = table.Column<DateTime>(
                            type: "timestamp without time zone",
                            nullable: false
                        ),
                        InsuranceCode = table.Column<string>(
                            type: "character varying(120)",
                            maxLength: 120,
                            nullable: true
                        ),
                        InsuranceProviderId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_PatientInsurers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientInsurers_InsuranceProviders_InsuranceProviderId",
                        column: x => x.InsuranceProviderId,
                        principalTable: "InsuranceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_PatientInsurers_InsuranceProviderId",
                table: "PatientInsurers",
                column: "InsuranceProviderId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_PatientInsurers_TenantId",
                table: "PatientInsurers",
                column: "TenantId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "PatientInsurers");
        }
    }
}
