using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedInvoiceEntityToIncludeAmountPaidAndOutstandingAmount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid_Amount",
                table: "Invoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmountPaid_Currency",
                table: "Invoices",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OutstandingAmount_Amount",
                table: "Invoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutstandingAmount_Currency",
                table: "Invoices",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountPaid_Amount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AmountPaid_Currency",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "OutstandingAmount_Amount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "OutstandingAmount_Currency",
                table: "Invoices");
        }
    }
}
