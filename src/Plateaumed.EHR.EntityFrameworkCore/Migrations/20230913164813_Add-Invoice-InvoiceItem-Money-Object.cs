using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Plateaumed.EHR.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceInvoiceItemMoneyObject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalDiscount",
                table: "PricingSettings");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalAmount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AmountPaid",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "DiscountAmount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "OutstandingAmount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "SubTotal",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "TopUpAmount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "InvoiceItems");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "ItemPricing",
                newName: "Amount_Amount");

            migrationBuilder.AlterColumn<string>(
                name: "Balance_Currency",
                table: "Wallets",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance_Amount",
                table: "Wallets",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentBalance_Currency",
                table: "WalletHistories",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentBalance_Amount",
                table: "WalletHistories",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Amount_Currency",
                table: "WalletHistories",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount_Amount",
                table: "WalletHistories",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric",
                oldNullable: true);

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

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount_Amount",
                table: "ItemPricing",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2);

            migrationBuilder.AddColumn<string>(
                name: "Amount_Currency",
                table: "ItemPricing",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage_Amount",
                table: "Invoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountPercentage_Currency",
                table: "Invoices",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount_Amount",
                table: "Invoices",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "TotalAmount_Currency",
                table: "Invoices",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid_Amount",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AmountPaid_Currency",
                table: "InvoiceItems",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount_Amount",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DiscountAmount_Currency",
                table: "InvoiceItems",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OutstandingAmount_Amount",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OutstandingAmount_Currency",
                table: "InvoiceItems",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal_Amount",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SubTotal_Currency",
                table: "InvoiceItems",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TopUpAmount_Amount",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopUpAmount_Currency",
                table: "InvoiceItems",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice_Amount",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UnitPrice_Currency",
                table: "InvoiceItems",
                type: "character varying(3)",
                maxLength: 3,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GlobalDiscount_Amount",
                table: "PricingSettings");

            migrationBuilder.DropColumn(
                name: "GlobalDiscount_Currency",
                table: "PricingSettings");

            migrationBuilder.DropColumn(
                name: "Amount_Currency",
                table: "ItemPricing");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage_Amount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "DiscountPercentage_Currency",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalAmount_Amount",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "TotalAmount_Currency",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "AmountPaid_Amount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "AmountPaid_Currency",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "DiscountAmount_Amount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "DiscountAmount_Currency",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "OutstandingAmount_Amount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "OutstandingAmount_Currency",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "SubTotal_Amount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "SubTotal_Currency",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "TopUpAmount_Amount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "TopUpAmount_Currency",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "UnitPrice_Amount",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "UnitPrice_Currency",
                table: "InvoiceItems");

            migrationBuilder.RenameColumn(
                name: "Amount_Amount",
                table: "ItemPricing",
                newName: "Amount");

            migrationBuilder.AlterColumn<string>(
                name: "Balance_Currency",
                table: "Wallets",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance_Amount",
                table: "Wallets",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CurrentBalance_Currency",
                table: "WalletHistories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentBalance_Amount",
                table: "WalletHistories",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Amount_Currency",
                table: "WalletHistories",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(3)",
                oldMaxLength: 3,
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount_Amount",
                table: "WalletHistories",
                type: "numeric",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "GlobalDiscount",
                table: "PricingSettings",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "ItemPricing",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "numeric(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountPercentage",
                table: "Invoices",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalAmount",
                table: "Invoices",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AmountPaid",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DiscountAmount",
                table: "InvoiceItems",
                type: "numeric",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "OutstandingAmount",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "SubTotal",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TopUpAmount",
                table: "InvoiceItems",
                type: "numeric(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "UnitPrice",
                table: "InvoiceItems",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
