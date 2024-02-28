using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class ModifyInvoiceItemDebtRelief : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
      
            migrationBuilder.AddColumn<decimal>(
                name: "DebtReliefAmount_Amount",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DebtReliefAmount_Currency",
                table: "InvoiceItems",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DebtReliefAmount_Amount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "DebtReliefAmount_Currency",
                table: "InvoiceItems");

        }
    }
}
