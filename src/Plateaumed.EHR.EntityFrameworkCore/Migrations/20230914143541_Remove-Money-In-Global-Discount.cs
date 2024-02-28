using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMoneyInGlobalDiscount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalDiscount_Amount",
                table: "PricingSettings");

            migrationBuilder.DropColumn(
                name: "GlobalDiscount_Currency",
                table: "PricingSettings");

            migrationBuilder.AddColumn<decimal>(
                name: "GlobalDiscount",
                table: "PricingSettings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalDiscount",
                table: "PricingSettings");

            migrationBuilder.AddColumn<decimal>(
                name: "GlobalDiscount_Amount",
                table: "PricingSettings",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlobalDiscount_Currency",
                table: "PricingSettings",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);
        }
    }
}
