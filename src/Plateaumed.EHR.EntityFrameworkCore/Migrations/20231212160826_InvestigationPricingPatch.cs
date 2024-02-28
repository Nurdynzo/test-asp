using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class InvestigationPricingPatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceCategoryDiscounts_PricingDiscountSettings_PricingDisco~",
                table: "PriceCategoryDiscounts");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "InvestigationPricing");

            migrationBuilder.AlterColumn<long>(
                name: "PricingDiscountSettingId",
                table: "PriceCategoryDiscounts",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PriceCategoryDiscounts_PricingDiscountSettings_PricingDisco~",
                table: "PriceCategoryDiscounts",
                column: "PricingDiscountSettingId",
                principalTable: "PricingDiscountSettings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PriceCategoryDiscounts_PricingDiscountSettings_PricingDisco~",
                table: "PriceCategoryDiscounts");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "InvestigationPricing");

            migrationBuilder.AlterColumn<long>(
                name: "PricingDiscountSettingId",
                table: "PriceCategoryDiscounts",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<long>(
                name: "CreatedByUserId",
                table: "InvestigationPricing",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "InvestigationPricing",
                type: "text",
                nullable: true,
                defaultValue: "True");

            migrationBuilder.AddForeignKey(
                name: "FK_PriceCategoryDiscounts_PricingDiscountSettings_PricingDisco~",
                table: "PriceCategoryDiscounts",
                column: "PricingDiscountSettingId",
                principalTable: "PricingDiscountSettings",
                principalColumn: "Id");
        }
    }
}
