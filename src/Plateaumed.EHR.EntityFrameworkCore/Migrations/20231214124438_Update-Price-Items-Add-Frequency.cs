using Microsoft.EntityFrameworkCore.Migrations;
using Plateaumed.EHR.Invoices;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePriceItemsAddFrequency : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<PriceTimeFrequency>(
                name: "Frequency",
                table: "ItemPricing",
                type: "price_time_frequency",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Frequency",
                table: "ItemPricing");
        }
    }
}
