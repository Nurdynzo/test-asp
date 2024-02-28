using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Misc;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdateItemPricingAddPriceCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<PricingCategory>(
                name: "PricingCategory",
                table: "ItemPricing",
                type: "pricing_category",
                nullable: false,
                defaultValue: PricingCategory.Consultation);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PricingCategory",
                table: "ItemPricing");
        }
    }
}
