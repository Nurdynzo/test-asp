using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ItemCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Facilities_PricingRules_PricingRuleId",
                table: "Facilities");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemPricing_Facilities_FacilityId",
                table: "ItemPricing");

            migrationBuilder.DropTable(
                name: "PricingRules");

            migrationBuilder.DropIndex(
                name: "IX_Facilities_PricingRuleId",
                table: "Facilities");

            migrationBuilder.DropColumn(
                name: "PricingRuleId",
                table: "Facilities");

            migrationBuilder.RenameColumn(
                name: "FacilityId",
                table: "ItemPricing",
                newName: "ItemPricingCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPricing_FacilityId",
                table: "ItemPricing",
                newName: "IX_ItemPricing_ItemPricingCategoryId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "ItemPricing",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(12,2)",
                oldPrecision: 12,
                oldScale: 2);

            migrationBuilder.CreateTable(
                name: "ItemPricingCategories",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying(120)", maxLength: 120, nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "numeric", nullable: false),
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
                    table.PrimaryKey("PK_ItemPricingCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PricingSettings",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GlobalDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    TenantId = table.Column<int>(type: "integer", nullable: false),
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
                    table.PrimaryKey("PK_PricingSettings", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPricing_ItemPricingCategories_ItemPricingCategoryId",
                table: "ItemPricing",
                column: "ItemPricingCategoryId",
                principalTable: "ItemPricingCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ItemPricing_ItemPricingCategories_ItemPricingCategoryId",
                table: "ItemPricing");

            migrationBuilder.DropTable(
                name: "ItemPricingCategories");

            migrationBuilder.DropTable(
                name: "PricingSettings");

            migrationBuilder.RenameColumn(
                name: "ItemPricingCategoryId",
                table: "ItemPricing",
                newName: "FacilityId");

            migrationBuilder.RenameIndex(
                name: "IX_ItemPricing_ItemPricingCategoryId",
                table: "ItemPricing",
                newName: "IX_ItemPricing_FacilityId");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "ItemPricing",
                type: "numeric(12,2)",
                precision: 12,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<long>(
                name: "PricingRuleId",
                table: "Facilities",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PricingRules",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CreationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    InvoiceDiscount = table.Column<decimal>(type: "numeric", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    TenantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PricingRules", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Facilities_PricingRuleId",
                table: "Facilities",
                column: "PricingRuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Facilities_PricingRules_PricingRuleId",
                table: "Facilities",
                column: "PricingRuleId",
                principalTable: "PricingRules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ItemPricing_Facilities_FacilityId",
                table: "ItemPricing",
                column: "FacilityId",
                principalTable: "Facilities",
                principalColumn: "Id");
        }
    }
}
