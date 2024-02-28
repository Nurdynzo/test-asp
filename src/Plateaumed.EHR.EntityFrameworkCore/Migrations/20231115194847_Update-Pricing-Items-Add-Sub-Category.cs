﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePricingItemsAddSubCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubCategory",
                table: "ItemPricing",
                type: "character varying(250)",
                maxLength: 250,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubCategory",
                table: "ItemPricing");
        }
    }
}
