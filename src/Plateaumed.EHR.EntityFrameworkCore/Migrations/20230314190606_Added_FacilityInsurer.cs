using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddedFacilityInsurer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FacilityInsurers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    FacilityGroupId = table.Column<long>(type: "bigint", nullable: true),
                    FacilityId = table.Column<long>(type: "bigint", nullable: true),
                    InsuranceProviderId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_FacilityInsurers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FacilityInsurers_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FacilityInsurers_FacilityGroups_FacilityGroupId",
                        column: x => x.FacilityGroupId,
                        principalTable: "FacilityGroups",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_FacilityInsurers_InsuranceProviders_InsuranceProviderId",
                        column: x => x.InsuranceProviderId,
                        principalTable: "InsuranceProviders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FacilityInsurers_FacilityGroupId",
                table: "FacilityInsurers",
                column: "FacilityGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityInsurers_FacilityId",
                table: "FacilityInsurers",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityInsurers_InsuranceProviderId",
                table: "FacilityInsurers",
                column: "InsuranceProviderId");

            migrationBuilder.CreateIndex(
                name: "IX_FacilityInsurers_TenantId",
                table: "FacilityInsurers",
                column: "TenantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FacilityInsurers");
        }
    }
}
