using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Plateaumed.EHR.Invoices;
using Plateaumed.EHR.Misc;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddExtendedPriceSettings : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PricingSettings");

            migrationBuilder.CreateTable(
                name: "PriceMealSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    FacilityId = table.Column<long>(type: "bigint", nullable: false),
                    DefaultInitialPeriodValue = table.Column<int>(type: "integer", nullable: false),
                    DefaultInitialPeriodUnit = table.Column<PriceTimeFrequency>(type: "price_time_frequency", nullable: false),
                    DefaultContinuePeriodUnit = table.Column<PriceTimeFrequency>(type: "price_time_frequency", nullable: false),
                    DefaultContinuePeriodValue = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_PriceMealSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceMealSettings_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceWardAdmissionSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    FacilityId = table.Column<long>(type: "bigint", nullable: false),
                    DefaultInitialPeriodValue = table.Column<int>(type: "integer", nullable: false),
                    DefaultInitialPeriodUnit = table.Column<PriceTimeFrequency>(type: "price_time_frequency", nullable: false),
                    DefaultContinuePeriodUnit = table.Column<PriceTimeFrequency>(type: "price_time_frequency", nullable: false),
                    DefaultContinuePeriodValue = table.Column<int>(type: "integer", nullable: false),
                    AdmissionChargeTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    FollowUpVisitPercentage = table.Column<decimal>(type: "numeric", nullable: false),
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
                    table.PrimaryKey("PK_PriceWardAdmissionSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceWardAdmissionSettings_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PricingDiscountSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GlobalDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    FacilityId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_PricingDiscountSettings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PricingDiscountSettings_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PriceMealPlanDefinitions",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    FacilityId = table.Column<long>(type: "bigint", nullable: false),
                    PlanType = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    Description = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    PriceMealSettingId = table.Column<long>(type: "bigint", nullable: false),
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
                    table.PrimaryKey("PK_PriceMealPlanDefinitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceMealPlanDefinitions_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PriceMealPlanDefinitions_PriceMealSettings_PriceMealSetting~",
                        column: x => x.PriceMealSettingId,
                        principalTable: "PriceMealSettings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PriceCategoryDiscounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    FacilityId = table.Column<long>(type: "bigint", nullable: true),
                    PricingCategory = table.Column<PricingCategory>(type: "pricing_category", nullable: false),
                    Percentage = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    PricingDiscountSettingId = table.Column<long>(type: "bigint", nullable: true),
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
                    table.PrimaryKey("PK_PriceCategoryDiscounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceCategoryDiscounts_Facilities_FacilityId",
                        column: x => x.FacilityId,
                        principalTable: "Facilities",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PriceCategoryDiscounts_PricingDiscountSettings_PricingDisco~",
                        column: x => x.PricingDiscountSettingId,
                        principalTable: "PricingDiscountSettings",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceCategoryDiscounts_FacilityId",
                table: "PriceCategoryDiscounts",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceCategoryDiscounts_PricingDiscountSettingId",
                table: "PriceCategoryDiscounts",
                column: "PricingDiscountSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceMealPlanDefinitions_FacilityId",
                table: "PriceMealPlanDefinitions",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceMealPlanDefinitions_PriceMealSettingId",
                table: "PriceMealPlanDefinitions",
                column: "PriceMealSettingId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceMealSettings_FacilityId",
                table: "PriceMealSettings",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceWardAdmissionSettings_FacilityId",
                table: "PriceWardAdmissionSettings",
                column: "FacilityId");

            migrationBuilder.CreateIndex(
                name: "IX_PricingDiscountSettings_FacilityId",
                table: "PricingDiscountSettings",
                column: "FacilityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceCategoryDiscounts");

            migrationBuilder.DropTable(
                name: "PriceMealPlanDefinitions");

            migrationBuilder.DropTable(
                name: "PriceWardAdmissionSettings");

            migrationBuilder.DropTable(
                name: "PricingDiscountSettings");

            migrationBuilder.DropTable(
                name: "PriceMealSettings");

            migrationBuilder.CreateTable(
                name: "PricingSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    GlobalDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingSettings", x => x.Id);
                });
        }
    }
}
